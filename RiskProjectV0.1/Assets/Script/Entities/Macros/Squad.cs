using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A squad is composed of minimum an unit, it is the unit group for macro game
/// </summary>
public class Squad {

    public List<UnitData> Units { get; private set; }
    public int Team { get; private set; }

    private GameObject gameObject;

    /// <summary>
    /// Move the position of the squad
    /// </summary>
    /// <param name="to"></param>
    public void Move(Vector3 to) {
        to.y = gameObject.transform.position.y;
        gameObject.transform.position = to;
    }

    /// <summary>
    /// Create the squad
    /// </summary>
    /// <param name="units">units insides</param>
    /// <param name="team">team</param>
    /// <param name="position">where it is create</param>
    public Squad(List<UnitData> units, int team, Vector3 position) {
        Units = units;
        Team = team;


        switch (Team) {
            case 1:
                gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Macros/SquadP1"));
                break;
            case 2:
                gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Macros/SquadP2"));
                break;
            case 3:
                gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Macros/SquadP3"));
                break;
            default:
                break;
        }
        position.y = gameObject.transform.position.y;
        gameObject.transform.position = position;
    }

    /// <summary>
    /// Add an unit to the squad
    /// </summary>
    /// <param name="unit">the unit to add</param>
    public void AddUnit(UnitData unit) {
        Units.Add(unit);
    }

    /// <summary>
    /// add a list of unit to the squad
    /// </summary>
    /// <param name="units">units to adds</param>
    public void AddUnits(List<UnitData> units) {
        foreach (UnitData unit in units) {
            Units.Add(unit);
        }
    }

    /// <summary>
    /// switch the actual units of the squad for an other one
    /// </summary>
    /// <param name="units">units to switch</param>
    public void UpdateUnits(List<UnitData> units) {
        Units = units;
    }

    /// <summary>
    /// remove an unit from the squad
    /// will destroy the squad if empty
    /// </summary>
    /// <param name="unit">the unit to remove</param>
    public void RemoveUnit(UnitData unit) {
        Units.Remove(unit);
        if (Units.Count == 0) {
            Destroy();
        }
    }

    /// <summary>
    /// Remove a list of units
    /// destroy squad if empty
    /// </summary>
    /// <param name="units"></param>
    public void RemoveUnits(List<UnitData> units) {
        foreach (UnitData unit in units) {
            Units.Remove(unit);
        }
        if (Units.Count == 0) {
            Destroy();
        }
    }



    public void Destroy() {
        //TODO Properly destroy this, create as pool maybe ._.
        Debug.Log("Destroy this squad");
        GameObject.Destroy(gameObject);
    }

}
