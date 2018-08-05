using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic area, every city and outpost are capturable
/// </summary>
public class Capturable : MonoBehaviour {

    [SerializeField]
    public int team;

    public List<Squad> Units { get; private set; }

    [SerializeField]
    public string MapName;
    public bool HasBuilding;
    public bool Test;

    public void LaunchBattle() {
        BattleParams _bp = new BattleParams(MapName, Units[0], Units[1]);
        BattlefieldManager.Instance.SetNextBattle(_bp);
        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.BattleScene);
    }

    void Awake() {
        Units = new List<Squad>();

        if (Test) {
            List<UnitData> unitsT1 = new List<UnitData>();
            unitsT1.Add(new UnitData(team, GV.Units.Lancer, 1, 1, 1, 0, 1, 3, 10, 10, false));
            unitsT1.Add(new UnitData(team, GV.Units.Axeman, 1, 1, 1, 0, 1, 3, 10, 10, false));
            unitsT1.Add(new UnitData(team, GV.Units.Swordman, 1, 1, 1, 0, 1, 3, 10, 10, false));
            Squad team1 = new Squad(unitsT1, 1, gameObject.transform.position);

            List<UnitData> unitsT2 = new List<UnitData>();
            unitsT2.Add(new UnitData(team++.Clamp(1,3), GV.Units.Lancer, 1, 1, 1, 0, 1, 3, 10, 10, false));
            Squad team2 = new Squad(unitsT2, 1, gameObject.transform.position);

            Units.Add(team1);
            Units.Add(team2);
        }

    }

    /// <summary>
    /// Create a new squat from an unit Data
    /// Usefull to divide squads
    /// </summary>
    /// <param name="UD">The unit data of the first unit in the new squad</param>
    public void CreateSquad(UnitData UD) {
        List<UnitData> UList = new List<UnitData>();

        UList.Add(UD);

        Units.Add(new Squad(UList, team, gameObject.transform.position));
    }

    /// <summary>
    /// A capurable can contain multiples squads
    /// </summary>
    /// <param name="squad">the squad you wand add</param>
    public void AddSquad(Squad squad) {
        Units.Add(squad);
    }

    /// <summary>
    /// Move a squad from tyhe actual capturable to an other 
    /// </summary>
    /// <param name="squad"></param>
    /// <param name="to"></param>
    public void MoveSquad(Squad squad, Capturable to) {
        to.AddSquad(squad);
        Units.Remove(squad);
        squad.Move(to.gameObject.transform.position);
        //Debug.Log("squad moved");
    }

}
