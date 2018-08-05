using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// In macro game UI management
/// </summary>
public class MacroUI : IBasicUI {

    GameObject choicePanel;
    GameObject choice1;
    GameObject choice2;
    GameObject choice3;
    GameObject choice4;
    GameObject choice5;
    RectTransform selectorC;
    List<Utils.KVPaires<GameObject, Choice>> Choices;
    int iteratorChoices = 0;

    GameObject selectPanel;
    Text name;
    Text hp;
    Text attack;
    Text defence;
    Text speed;
    Text exp;
    Text level;

    GameObject turnPanel;
    Text turn;

    GameObject menuPanel;
    RectTransform selectorM;
    int iteratorMenu = 0;
    GameObject endTurn;
    GameObject option;
    GameObject save;
    GameObject exit;

    GameObject popupPanel;
    Text popupText;

    GameObject capturablePanel;
    Text nameC;
    Text teamC;
    Text building;
    Text units;


    Dictionary<MacroUIPanels, GameObject> panels;


    public void Start() {
        //*******************************//
        // GRAB COMPONENTS
        //*******************************//
        choicePanel = GameObject.Find("choicePanel");
        choice1 = GameObject.Find("Choice1");
        choice2 = GameObject.Find("Choice2");
        choice3 = GameObject.Find("Choice3");
        choice4 = GameObject.Find("Choice4");
        choice5 = GameObject.Find("Choice5");
        choice5 = GameObject.Find("Choice5");
        selectorC = GameObject.Find("SelectorC").GetComponent<RectTransform>();


        selectPanel = GameObject.Find("selectPanel");
        name = GameObject.Find("Name").GetComponent<Text>();
        hp = GameObject.Find("HP").GetComponent<Text>();
        attack = GameObject.Find("Attack").GetComponent<Text>();
        defence = GameObject.Find("Defence").GetComponent<Text>();
        speed = GameObject.Find("Speed").GetComponent<Text>();
        exp = GameObject.Find("Experience").GetComponent<Text>();
        level = GameObject.Find("Level").GetComponent<Text>();

        turnPanel = GameObject.Find("turnPanel");
        turn = GameObject.Find("turnText").GetComponent<Text>();

        menuPanel = GameObject.Find("menuPanel");
        selectorM = GameObject.Find("SelectorM").GetComponent<RectTransform>();
        endTurn = GameObject.Find("end");
        option = GameObject.Find("option");
        save = GameObject.Find("save");
        exit = GameObject.Find("exit");

        popupPanel = GameObject.Find("popupPanel");
        popupText = GameObject.Find("PopupText").GetComponent<Text>();

        capturablePanel = GameObject.Find("capturablePanel");
        nameC = GameObject.Find("NameC").GetComponent<Text>();
        teamC = GameObject.Find("TeamC").GetComponent<Text>();
        building = GameObject.Find("Building").GetComponent<Text>();
        units = GameObject.Find("Units").GetComponent<Text>();

        //*******************************//
        // ORGANIZE
        //*******************************//
        panels = new Dictionary<MacroUIPanels, GameObject>();
        panels.Add(MacroUIPanels.choice, choicePanel);
        panels.Add(MacroUIPanels.select, selectPanel);
        panels.Add(MacroUIPanels.turn, turnPanel);
        panels.Add(MacroUIPanels.menu, menuPanel);
        panels.Add(MacroUIPanels.popup, popupPanel);

        panels[MacroUIPanels.choice].SetActive(false);
        panels[MacroUIPanels.select].SetActive(false);
        panels[MacroUIPanels.menu].SetActive(false);
        panels[MacroUIPanels.popup].SetActive(false);

        Choices = new List<Utils.KVPaires<GameObject, Choice>>() {
            new Utils.KVPaires<GameObject, Choice>(choice1, null),
            new Utils.KVPaires<GameObject, Choice>(choice2, null),
            new Utils.KVPaires<GameObject, Choice>(choice3, null),
            new Utils.KVPaires<GameObject, Choice>(choice4, null),
            new Utils.KVPaires<GameObject, Choice>(choice5, null)
        };
    }

    /// <summary>
    /// Make appears a "responsive" list of choices
    /// </summary>
    /// <param name="choices">the choices you want sent to the panels</param>
    public void UpChoices(List<Choice> choices) {
        panels[MacroUIPanels.choice].SetActive(true);

        for (int i = 0; i < this.Choices.Count; ++i) {
            if (i < choices.Count) {
                this.Choices[i].Value = choices[i];
                this.Choices[i].Key.GetComponent<Text>().text = choices[i].Name;
            }
            else {
                this.Choices[i].Value = null;
            }
            this.Choices[i].Key.SetActive(i < choices.Count);
            panels[MacroUIPanels.choice].GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 5 + (choices.Count * 30));
        }
    }

    /// <summary>
    /// turn off the panel
    /// </summary>
    public void HideChoices() {
        panels[MacroUIPanels.choice].SetActive(false);
    }

    /// <summary>
    /// activate the menu
    /// </summary>
    public void ShowMenu() {
        panels[MacroUIPanels.menu].SetActive(true);
    }

    /// <summary>
    /// hide the menu
    /// </summary>
    public void HideMenu() {
        panels[MacroUIPanels.menu].SetActive(false);
    }

    /// <summary>
    /// shiw unit info to the player
    /// </summary>
    /// <param name="unit">unit show by the UI</param>
    public void ShowSelect(Unit unit) {
        panels[MacroUIPanels.select].SetActive(true);
        name.text = unit.Type.ToString();
        hp.text = unit.actualHP.ToString() + "/" + unit.maxHP.ToString();
        attack.text = unit.attack.ToString();
        defence.text = unit.defense.ToString();
        speed.text = unit.speed.ToString();

        if (unit.experience != -1)
            exp.text = unit.experience.ToString() + "/" + unit.expNextLvl.ToString();
        else exp.text = "max";

        level.text = unit.level.ToString();
    }

    /// <summary>
    /// Hide unit info panel
    /// </summary>
    public void HideSelect() {
        panels[MacroUIPanels.select].SetActive(false);
    }

    /// <summary>
    /// Show a pop up with an OK button
    /// </summary>
    /// <param name="message">message shown</param>
    public void PopMessage(string message) {
        popupText.text = message;
        popupPanel.SetActive(true);
    }

    /// <summary>
    /// update thr turn pannel
    /// </summary>
    /// <param name="text">message appears</param>
    public void UpdateTurnPanel(string text) {
        //Debug.Log("in tp : " + text);
        turn.text = text;
    }

    /// <summary>
    /// return true if you are not in an UI state
    /// </summary>
    /// <returns>if false, you can move the game cursor</returns>
    public bool IsPlayable() {
        return !panels[MacroUIPanels.choice].activeInHierarchy && !panels[MacroUIPanels.menu].activeInHierarchy;
    }

    public void Update(InputParams input) {

        //*********************************//
        // CHOICE PANEL MANAGEMENT
        //*********************************//
        
        if (panels[MacroUIPanels.choice].activeInHierarchy && !panels[MacroUIPanels.menu].activeInHierarchy) {

            if (Choices.GetCountNotNull() == 0) {
                //Debug.Log("Error hav been catch");
                panels[MacroUIPanels.choice].SetActive(false);
            }

            IteratorChoiceUpdate();
            if (input.Direction.y > 0) {
                iteratorChoices++;
                iteratorChoices = iteratorChoices.Clamp(Choices.GetCountNotNull() - 1);
            }
            else if (input.Direction.y < 0) {
                iteratorChoices--;
                iteratorChoices = iteratorChoices.Clamp(Choices.GetCountNotNull() - 1);
            }
            if (input.Confirm) {
                try {
                    Choices[iteratorChoices].Value.Call();
                }
                catch {
                    Debug.Log("iteratorChoices : " + iteratorChoices);
                    Debug.Log("Clamp : " + iteratorChoices.Clamp(Choices.GetCountNotNull() - 1));
                }
            }
            if (input.Cancel) {
                HideChoices();
            }

            input.Use();
        }


        //*********************************//
        // MENU PANEL MANAGEMENT
        //*********************************//
        else if (panels[MacroUIPanels.menu].activeInHierarchy) {
            if (input.Direction.y > 0) {
                iteratorMenu++;
            }
            else if (input.Direction.y < 0) {
                iteratorMenu--;
            }
            IteratorMenuUpdate();
            if (input.Confirm) {
                switch (iteratorMenu) {
                    case 0:
                        Exit();
                        HideMenu();
                        break;
                    case 1:
                        Save();
                        HideMenu();
                        break;
                    case 2:
                        HideMenu();
                        Option();
                        break;
                    case 3:
                        HideMenu();
                        End();
                        break;
                    default:
                        break;
                }
            }

            if (input.Cancel) {
                HideMenu();
            }
        }
        else {
            if (input.Cancel) {
                ShowMenu();
            }
        }
    }

    /// <summary>
    /// clamp the iterator, then set the selector for the choice panel
    /// </summary>
    private void IteratorChoiceUpdate() {
        iteratorChoices.Clamp(Choices.GetCountNotNull() - 1);
        selectorC.localPosition = new Vector3(145, iteratorChoices * 30, 0);
    }

    /// <summary>
    /// clamp the iterator, then set the selector for the menu panel
    /// </summary>
    private void IteratorMenuUpdate() {
        iteratorMenu.Clamp(3);
        selectorM.localPosition = new Vector3(50, -47 + (iteratorMenu * 30), 0);
    }

    /// <summary>
    /// show info about the actuale area selected
    /// </summary>
    /// <param name="capturable">the area you want show</param>
    public void ShowCapturable(Capturable capturable) {
        capturablePanel.SetActive(true);

        nameC.text = capturable.name;
        teamC.text = capturable.team.ToString();
        this.building.text = capturable.HasBuilding? "Yes" : "No";
        if (capturable.Units.Count>0) {
            this.units.text = "Yes";
        }
        else {
            this.units.text = "No";
        }
    }

    /// <summary>
    /// hide the information pannel
    /// </summary>
    public void HideCapturable() {
        capturablePanel.SetActive(false);
    }

    /// <summary>
    /// end the turn
    /// </summary>
    private void End() {
        MacroManager.Instance.EndTurn();
    }

    /// <summary>
    /// Open the option pannel
    /// </summary>
    private void Option() {
        //TODO Option panel
        //Debug.Log("Option Called");

    }
    private void Save() {
        //TODO call save tool
        //Debug.Log("Save Called");

    }

    /// <summary>
    /// Quit the application 
    /// </summary>
    private void Exit() {
        Application.Quit();

    }
}

public enum MacroUIPanels { choice, select, turn, menu, popup };