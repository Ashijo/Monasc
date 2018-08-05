using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage the macro game
/// </summary>
public class MacroManager {

    #region Singleton
    private static MacroManager instance;

    private MacroManager() { }

     public static MacroManager Instance {
        get {
            if (instance == null) {
                instance = new MacroManager();
            }
            return instance;
        }
    }

    #endregion 

    private List<GameObject> Cities;
    private List<GameObject> Outpost;
    private List<Capturable> Capturables;
    private List<Road> Roads;

    private GameObject SelectObject;
    private GameObject Selector;

    public int Turn { get; private set; }
    public int Player { get; private set; }

    MacroUI UI;
    public int Iterator { get; private set; }
    public List<GameObject> NearLoc;
    public GameObject ShowedNearLoc;

    Squad selectedSquad;
    bool MoveSquad = false;


    // Use this for initialization
    public void Start () {

        Cities = new List<GameObject>();
        Outpost = new List<GameObject>();
        Capturables = new List<Capturable>();
        Roads = new List<Road>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("City")) {
            Cities.Add(go);
            Capturables.Add(go.GetComponent<Capturable>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Outpost")) {
            Outpost.Add(go);
            Capturables.Add(go.GetComponent<Capturable>());
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Road")) {
            Roads.Add(go.GetComponent<Road>());
        }

        Turn = 1;
        Player = 1;

        UI = new MacroUI();
        UI.Start();

        Selector = GameObject.Find("BattlefieldSelector");
        Select(Cities[0]);
        NearLoc = GetClosestCapturables(SelectObject);

        UI.ShowCapturable(SelectObject.GetComponent<Capturable>());

    }

    public void Update(float _dt, InputParams _ip) {

        if (!_ip.Consumed) {
            UI.Update(_ip);
        }

        
        //If a squad isn't actually moving 
        if (!MoveSquad) {

            // Debug fast cursor travel
            if (UI.IsPlayable() && !_ip.Consumed) {
                if (_ip.onePressed) {
                    ShowedNearLoc = Cities[0];
                }
                if (_ip.twoPressed) {
                    ShowedNearLoc = Cities[1];
                }
                if (_ip.threePressed) {
                    ShowedNearLoc = Cities[2];
                }


                //Show diferent nearest locations 
                if (_ip.Direction.x != 0 || _ip.Direction.y != 0) {
                    if (ShowedNearLoc == null) {
                        Iterator = 0;
                    }
                    else if (_ip.Direction.x > 0) {
                        Iterator++;
                        Iterator = Iterator.Clamp(NearLoc.Count - 1);
                    }
                    else if (_ip.Direction.x < 0) {
                        Iterator--;
                        Iterator = Iterator.Clamp(NearLoc.Count - 1);
                    }
                    else if (_ip.Direction.y > 0) {
                        Iterator++;
                        Iterator = Iterator.Clamp(NearLoc.Count - 1);
                    }
                    else if (_ip.Direction.y < 0) {
                        Iterator--;
                        Iterator = Iterator.Clamp(NearLoc.Count - 1);
                    }
                    ShowedNearLoc = NearLoc[Iterator];

                    UI.ShowCapturable(ShowedNearLoc.GetComponent<Capturable>());
                    SuggestSelect(ShowedNearLoc);
                }
                // If comfirm pressed, go to the next lock
                if (_ip.Confirm) {
                    if (ShowedNearLoc != null)
                        Select();
                    else {


                        List<Choice> list = new List<Choice>();
                        if (SelectObject.GetComponent<Capturable>().HasBuilding) {
                            Choice CreateUnitC = new Choice("Create Unit", CreateUnit);
                            list.Add(CreateUnitC);
                        }
                        if (SelectObject.GetComponent<Capturable>().Units.Count > 0) {
                            Choice SelectSquadC = new Choice("Select Squad", SelectSquad);
                            list.Add(SelectSquadC);
                        }
                        if (SelectObject.GetComponent<Capturable>().Units.Count > 1) {
                            Choice Battle = new Choice("Battle", LaunchBattle);
                            list.Add(Battle);
                        }

                        UI.UpChoices(list);
                    }
                }
                _ip.Use();
            }
        }
        else if(!_ip.Consumed){
                if (ShowedNearLoc == null) {
                    Iterator = 0;
                }
                else if (_ip.Direction.x > 0) {
                    Iterator++;
                    Iterator = Iterator.Clamp(NearLoc.Count - 1);
                }
                else if (_ip.Direction.x < 0) {
                    Iterator--;
                    Iterator = Iterator.Clamp(NearLoc.Count - 1);
                }
                else if (_ip.Direction.y > 0) {
                    Iterator++;
                    Iterator = Iterator.Clamp(NearLoc.Count - 1);
                }
                else if (_ip.Direction.y < 0) {
                    Iterator--;
                    Iterator = Iterator.Clamp(NearLoc.Count - 1);
                }
                ShowedNearLoc = NearLoc[Iterator];

                UI.ShowCapturable(ShowedNearLoc.GetComponent<Capturable>());
                SuggestSelect(ShowedNearLoc);

            if (_ip.Confirm) {
                SelectObject.GetComponent<Capturable>().MoveSquad(selectedSquad, ShowedNearLoc.GetComponent<Capturable>());
                MoveSquad = false;
            }
        }


        if (_ip.Cancel) {
            //Debug.Log("Cancel called");
            if (UI.IsPlayable()) {
                UI.HideChoices();
                UI.HideMenu();
            }
            else {
                //Debug.Log("Menu called");
                UI.ShowMenu();
            }
        }
    }

    /// <summary>
    /// Launch a battle, must be pressable only if there is enemy squad on the same place 
    /// </summary>
    private void LaunchBattle() {
        SelectObject.GetComponent<Capturable>().LaunchBattle();
    }

    /// <summary>
    /// Select the squad on the actual place
    /// </summary>
    private void SelectSquad() {
        //TODO 
        //Manage the case of multiple squad on the same place
        selectedSquad = SelectObject.GetComponent<Capturable>().Units[0];
        MoveSquad = true;
        UI.HideChoices();

        //Debug.Log("Squad selected");
    }


    /// <summary>
    /// Create an unit panel
    /// </summary>
    private void CreateUnit() {
        List<Choice> choices = new List<Choice>();
        choices.Add(new Choice("Lancer", CreateLancer));
        choices.Add(new Choice("Axeman", CreateAxeman));
        choices.Add(new Choice("Swordman", CreateSwordman));
        UI.UpChoices(choices);
    }

    /// <summary>
    /// Create a lancer
    /// </summary>
    private void CreateLancer() {
        if (SelectObject.GetComponent<Capturable>().Units.Count != 0 && SelectObject.GetComponent<Capturable>().Units[0].Team == Player) {
            SelectObject.GetComponent<Capturable>().Units[0].AddUnit(new UnitData(Player, GV.Units.Lancer, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        else {
            SelectObject.GetComponent<Capturable>().CreateSquad(new UnitData(Player, GV.Units.Lancer, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        UI.HideChoices();
    }


    /// <summary>
    /// Create an axeman
    /// </summary>
    private void CreateAxeman() {
        if (SelectObject.GetComponent<Capturable>().Units.Count != 0 && SelectObject.GetComponent<Capturable>().Units[0].Team == Player) {
            SelectObject.GetComponent<Capturable>().Units[0].AddUnit(new UnitData(Player, GV.Units.Axeman, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        else {
            SelectObject.GetComponent<Capturable>().CreateSquad(new UnitData(Player, GV.Units.Axeman, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        UI.HideChoices();
    }

    /// <summary>
    /// Create a swordman
    /// </summary>
    private void CreateSwordman() {
        if (SelectObject.GetComponent<Capturable>().Units.Count != 0 && SelectObject.GetComponent<Capturable>().Units[0].Team == Player) {
            SelectObject.GetComponent<Capturable>().Units[0].AddUnit(new UnitData(Player, GV.Units.Swordman, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        else {
            SelectObject.GetComponent<Capturable>().CreateSquad(new UnitData(Player, GV.Units.Swordman, 1, 1, 1, 0, 1, 3, 10, 10, false));
        }
        UI.HideChoices();
    }

    /// <summary>
    /// Select the location actualy showed by the cursor
    /// </summary>
    private void Select() {
        Select(ShowedNearLoc);
        UI.HideChoices();
        NearLoc = GetClosestCapturables(SelectObject);
        Iterator = 0;
        ShowedNearLoc = null;
    }

    /// <summary>
    /// End the turn
    /// </summary>
    public void EndTurn() {
        if (Player == 3) {
            Player = 1;
            Turn++;
        }
        else {
            Player++;
        }
        //Debug.Log("Turn " + Turn + " / Player " + Player);
        UI.UpdateTurnPanel("Turn " + Turn + " / Player " + Player);
    }

    /// <summary>
    /// check the closest capturable linked by roads
    /// </summary>
    /// <param name="cpt">the central game object</param>
    /// <returns>list of every game object like to the central one by roads</returns>
    private List<GameObject> GetClosestCapturables(GameObject cpt) {

        List<GameObject> backer = new List<GameObject>();

        foreach (Road r in Roads) {
            if (r.GetA() == cpt.gameObject) {
                backer.Add(r.GetB());
            }
            else if(r.GetB() == cpt.gameObject) {
                backer.Add(r.GetA());
            }
        }

        return backer;
    }

    /// <summary>
    /// Select an object
    /// </summary>
    /// <param name="capturable">gameobject to select</param>
    public void Select(GameObject capturable){
        SelectObject = capturable;
        Vector3 pos = SelectObject.transform.position;
        pos.y = .5f;
        Selector.transform.position = pos;
    }

    public void SuggestSelect(GameObject capturable) {
        Vector3 pos = capturable.transform.position;
        pos.y = .5f;
        Selector.transform.position = pos;
    }
}
