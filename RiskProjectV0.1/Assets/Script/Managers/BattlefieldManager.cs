using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Battlefield manager will manage every thing on thew battlefield, including, the batllefield himself
/// </summary>
public class BattlefieldManager {

    #region Singleton
    private static BattlefieldManager instance;

    private BattlefieldManager() { }

    public static BattlefieldManager Instance {
        get {
            if (instance == null)
                instance = new BattlefieldManager();

            return instance;
        }
    }
    #endregion

    BattleUI UI;

    GameObject battlefield;

    int nbTurn;
    bool playerTurn;
    BattleParams NextBattle;

    GameObject selector;
    Vector2Int selectorPosition;
    Unit selectedOne;
    bool attackMod = false;

    /*
     * An old way to generate battlefield
    public void GenerateBattlefield(string name) {
        string json = File.ReadAllText("Assets/Resources/BattlefieldMaps/" + name + ".jmap");

        Map map = JsonUtility.FromJson<Map>(json);
        battlefield = new GameObject();
        battlefield.AddComponent<BattleField>();
        battlefield.GetComponent<BattleField>().GenerateBattlefield(map);
    }*/


        /// <summary>
        /// Actual way to generate battlefield
        /// will get information from battle params
        /// </summary>
    public void GenerateBattlefield() {
        UI = new BattleUI();
        UI.Start();

        string json = File.ReadAllText("Assets/Resources/BattlefieldMaps/" + NextBattle.Name + ".jmap");

        Map map = JsonUtility.FromJson<Map>(json);
        battlefield = new GameObject();
        battlefield.AddComponent<BattleField>();
        battlefield.GetComponent<BattleField>().GenerateBattlefield(map);

        
        List<CaseData> list = map.GetAllSpawnable();
        
        Stack<UnitData> UnitsTeam1 = NextBattle.Team1.Units.ToStack();
        Stack<UnitData> UnitsTeam2 = NextBattle.Team2.Units.ToStack();


        foreach (CaseData spawnableCase in list) {
            if (spawnableCase.GetTeam() == 1 && UnitsTeam1.Count > 0) {
                UnitData ud = UnitsTeam1.Pop();
                UnitsManager.Instance.CreateUnit(ud);
                UnitsManager.Instance.MoveUnit(ud.Id,battlefield.GetComponent<BattleField>().GetMapPos(spawnableCase.GetPosition()));
            }
            if (spawnableCase.GetTeam() == 2 && UnitsTeam2.Count > 0) {
                UnitData ud = UnitsTeam2.Pop();
                UnitsManager.Instance.CreateUnit(ud);
                UnitsManager.Instance.MoveUnit(ud.Id, battlefield.GetComponent<BattleField>().GetMapPos(spawnableCase.GetPosition()));
            }
            if (UnitsTeam1.Count == 0 && UnitsTeam2.Count == 0) {
                break;
            }
        }

        selector = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/BattlefieldSelector"));
        selector.transform.parent = battlefield.transform;

        selectorPosition = new Vector2Int(0, 0);
    }

    /// <summary>
    /// Update the position of the selector of the battlefield
    /// </summary>
    private void SelectorPosUpdate() {
        Vector3 position = battlefield.GetComponent<BattleField>().GetPos(selectorPosition);
        position.y = battlefield.GetComponent<BattleField>().GetHeight(selectorPosition);

        selector.transform.position = position;
    }


        
    public void Update(InputParams _ip) {
        Unit unit = UnitsManager.Instance.GetUnitAtPos(selectorPosition);
        if (unit != null) {
            UI.ShowSelect(unit);
        }
        else {
            UI.HideSelect();
        }

        if (!_ip.Consumed) {
            UI.Update(_ip);
        }

        if (UI.Playable && !_ip.Consumed) {
            if (_ip.Direction.x < 0) {
                if (selectorPosition.x > 0) {
                    selectorPosition.x--;
                }
            }
            else if (_ip.Direction.x > 0) {
                if (selectorPosition.x < battlefield.GetComponent<BattleField>().GetSize().x - 1) {
                    selectorPosition.x++;
                }
            }
            if (_ip.Direction.y < 0) {
                if (selectorPosition.y < battlefield.GetComponent<BattleField>().GetSize().y - 1) {
                    selectorPosition.y++;
                }
            }
            else if (_ip.Direction.y > 0) {
                if (selectorPosition.y > 0) {
                    selectorPosition.y--;
                }
            }

            SelectorPosUpdate();

            if (_ip.Confirm && unit != null && !attackMod) {
                selectedOne = unit;
                Choice move = new Choice("Move", MoveHandler);
                Choice attack = new Choice("Attack", AttackHandler);
                List<Choice> choices = new List<Choice>();
                choices.Add(move);
                choices.Add(attack);
                UI.UpChoices(choices);

            }
            else if (_ip.Confirm && selectedOne != null && !attackMod) {
                selectedOne.Move(battlefield.GetComponent<BattleField>().GetMapPos(selector.transform.position));
                selectedOne = null;
            }
            else if (_ip.Confirm && attackMod && unit != null) {
                attackMod = false;
                selectedOne.Attack(unit);
                selectedOne = null;
            }
            _ip.Use();
        }
    }


    private void MoveHandler() {
        attackMod = false;
        UI.HideChoices();
       // Debug.Log("should hide choice");
    }

    private void AttackHandler() {
        attackMod = true;
        UI.HideChoices();

    }

    public void SetNextBattle(BattleParams _bp) {
        NextBattle = _bp;
    }

    public Vector3 GetLocalPos(Vector2Int MapPos) {
        return battlefield.GetComponent<BattleField>().GetPos(MapPos);
    }

    public float GetHeight(Vector2Int MapPos) {
        return battlefield.GetComponent<BattleField>().GetHeight(MapPos);
    }

    public Transform GetBattlefieldTransform() {
        return battlefield.transform;
    }

    public string NextTurn() {
        playerTurn = !playerTurn;
        if (playerTurn) nbTurn++;
        string backer = playerTurn ? "player turn" : "enemy turn";
        return "Turn " + nbTurn + " / " + backer;
    }

}

public class BattleParams{
    public BattleParams(string name, Squad team1, Squad team2) {
        Name = name;
        Team1 = team1;
        Team2 = team2;
    }

    public string Name { get; private set; }
    public Squad Team1 { get; private set; }
    public Squad Team2 { get; private set; }
}

