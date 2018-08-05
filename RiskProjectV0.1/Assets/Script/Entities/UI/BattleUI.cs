using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// In battle UI manager
/// </summary>
public class BattleUI : IBasicUI {

    /**********************
    * Choice Panel 
    *  The choice panel is the in game 
    *  main UI, let you do every possible action
    ************************/
    GameObject choicePanel;
        GameObject choice1;
        GameObject choice2;
        GameObject choice3;
        GameObject choice4;
        GameObject choice5;
        RectTransform selectorC;
        List<Utils.KVPaires<GameObject, Choice>> Choices;
        int iteratorChoices = 0;

    /********************
     * Select panel 
     *  The unit information panel 
     *******************/
    GameObject selectPanel;
        Text name;
        Text hp;
        Text attack;
        Text defence;
        Text speed;
        Text exp;
        Text level;

    /*******************
     * Turn panel 
     *  show you the actual player and the number of turn
     ******************/
    GameObject turnPanel;
        Text turn;


    /*********************
     * Menu panel
     *  Alow you to access option panel
     *  save, quit the application
     *  and end a turn
     * *****************************************/
    GameObject menuPanel;
        RectTransform selectorM;
        int iteratorMenu = 0;
        GameObject endTurn;
        GameObject flee;
        GameObject option;
        GameObject save;
        GameObject exit;


    /******************
     * Popup panel 
     * Allow you to show message to user,
     * usefull to create a tutorial
     * ***************************/
    GameObject popupPanel;
        Text popupText;


    Dictionary<BattleUIPanels, GameObject> panels;
    public bool Playable { get; private set; }

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
            name    = GameObject.Find("Name").GetComponent<Text>();
            hp      = GameObject.Find("HP").GetComponent<Text>();
            attack  = GameObject.Find("Attack").GetComponent<Text>();
            defence = GameObject.Find("Defence").GetComponent<Text>();
            speed   = GameObject.Find("Speed").GetComponent<Text>();
            exp     = GameObject.Find("Experience").GetComponent<Text>();
            level   = GameObject.Find("Level").GetComponent<Text>();

        turnPanel   = GameObject.Find("turnPanel"); 
            turn    = GameObject.Find("Text").GetComponent<Text>();

        menuPanel       = GameObject.Find("menuPanel");
            selectorM   = GameObject.Find("SelectorM").GetComponent<RectTransform>();
            endTurn     = GameObject.Find("end");
            flee        = GameObject.Find("flee");
            option      = GameObject.Find("option");
            save        = GameObject.Find("save");
            exit        = GameObject.Find("exit");

        popupPanel = GameObject.Find("popupPanel");
            popupText = GameObject.Find("PopupText").GetComponent<Text>();

        //*******************************//
        // ORGANIZE
        //*******************************//
        panels = new Dictionary<BattleUIPanels, GameObject>();
        panels.Add(BattleUIPanels.choice, choicePanel);
        panels.Add(BattleUIPanels.select, selectPanel);
        panels.Add(BattleUIPanels.turn, turnPanel);
        panels.Add(BattleUIPanels.menu, menuPanel);
        panels.Add(BattleUIPanels.popup, popupPanel);

        panels[BattleUIPanels.choice].SetActive(false);
        panels[BattleUIPanels.select].SetActive(false);
        panels[BattleUIPanels.menu].SetActive(false);
        panels[BattleUIPanels.popup].SetActive(false);

        ResetChoices();

    }

    /// <summary>
    /// Set all choices to the choice pannel to null;
    /// </summary>
    private void ResetChoices() {
        Choices = new List<Utils.KVPaires<GameObject, Choice>>();
        Choices.Add(new Utils.KVPaires<GameObject, Choice>(choice1, null));
        Choices.Add(new Utils.KVPaires<GameObject, Choice>(choice2, null));
        Choices.Add(new Utils.KVPaires<GameObject, Choice>(choice3, null));
        Choices.Add(new Utils.KVPaires<GameObject, Choice>(choice4, null));
        Choices.Add(new Utils.KVPaires<GameObject, Choice>(choice5, null));
    }

    /// <summary>
    /// Make appears a "responsive" list of choices
    /// </summary>
    /// <param name="choices"></param>
    public void UpChoices(List<Choice> choices) {
        panels[BattleUIPanels.choice].SetActive(true);

        for (int i = 0; i < this.Choices.Count; ++i) {
            if (i < choices.Count) {
                this.Choices[i].Value = choices[i];
                this.Choices[i].Key.GetComponent<Text>().text = choices[i].Name;
            }
            else {
                this.Choices[i].Value = null;
            }
            this.Choices[i].Key.SetActive(i < choices.Count);
            panels[BattleUIPanels.choice].GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 5 + (choices.Count * 30));
        }

    }

    /// <summary>
    /// Hide and clean choices panel
    /// </summary>
    public void HideChoices() {
        panels[BattleUIPanels.choice].SetActive(false);
        ResetChoices(); 
    }

    /// <summary>
    /// Show menu panel
    /// </summary>
    public void ShowMenu() {
        panels[BattleUIPanels.menu].SetActive(true);
    }

    /// <summary>
    /// Hide the menu panel
    /// </summary>
    public void HideMenu() {
        panels[BattleUIPanels.menu].SetActive(false);
    }

    /// <summary>
    /// show information of an unit in the 
    /// select pannel
    /// </summary>
    /// <param name="unit">the unit inf shown</param>
    public void ShowSelect(Unit unit) {
        panels[BattleUIPanels.select].SetActive(true);
        name.text    = unit.Type.ToString();
        hp.text      = unit.actualHP.ToString() + "/" + unit.maxHP.ToString();
        attack.text  = unit.attack.ToString();
        defence.text = unit.defense.ToString();
        speed.text   = unit.speed.ToString();

        if (unit.experience != -1)
            exp.text = unit.experience.ToString() + "/" + unit.expNextLvl.ToString();
        else exp.text = "max";

        level.text = unit.level.ToString();
    }

    /// <summary>
    /// Hide the select panel 
    /// </summary>
    public void HideSelect() {
        panels[BattleUIPanels.select].SetActive(false);
    }

    /// <summary>
    /// Pop a message in the popup panel 
    /// </summary>
    /// <param name="message"></param>
    public void PopMessage(string message) {
        popupText.text = message;
        popupPanel.SetActive(true);
    }

    /// <summary>
    /// Update the message on the turn panel 
    /// </summary>
    /// <param name="text">the message on the turn panel</param>
    public void UpdateTurnPanel(string text) {
        turn.text = text;
    }

    public void Update(InputParams input) {
        //*********************************//
        // CHOICE PANEL MANAGEMENT
        //*********************************//
        if (panels[BattleUIPanels.choice].activeInHierarchy && !panels[BattleUIPanels.menu].activeInHierarchy) {

            if (Choices.GetCountNotNull() == 0) {
                Debug.Log("Error hav been catch");
                panels[BattleUIPanels.choice].SetActive(false);
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
        else if (panels[BattleUIPanels.menu].activeInHierarchy) {
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
                        Flee();
                        break;
                    case 4:
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

        if (panels[BattleUIPanels.choice].activeInHierarchy || panels[BattleUIPanels.menu].activeInHierarchy) {
            Playable = false;
        }
        else Playable = true;

    }

    /// <summary>
    /// clamp the iterator, then set the selector for the choice panel
    /// </summary>
    private void IteratorChoiceUpdate() {
        iteratorChoices = iteratorChoices.Clamp(Choices.GetCountNotNull() - 1);
        selectorC.localPosition = new Vector3(145, iteratorChoices * 30, 0);
    }

    /// <summary>
    /// clamp the iterator, then set the selector for the menu panel
    /// </summary>
    private void IteratorMenuUpdate() {
        iteratorMenu = iteratorMenu.Clamp(4);
        selectorM.localPosition = new Vector3(50, -60 + (iteratorMenu * 30), 0);
    }


    /// <summary>
    /// End the turn
    /// </summary>
    private void End() {
        UpdateTurnPanel(BattlefieldManager.Instance.NextTurn());
    }

    /// <summary>
    /// Flee the battle
    /// </summary>
    private void Flee() {
        //TODO 
        // Implement flee system
    }

    /// <summary>
    /// Open the option panel
    /// </summary>
    private void Option() {
        //TODO option menu
    }

    /// <summary>
    /// Save the curent game
    /// </summary>
    private void Save() {
        //TODO call the save manager
    }


    /// <summary>
    /// Quit the application
    /// </summary>
    private void Exit() {
        Application.Quit();
    }

}

public enum BattleUIPanels {choice, select, turn, menu, popup };
