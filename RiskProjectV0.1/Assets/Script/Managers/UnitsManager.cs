using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsManager {

    #region Singleton
    private static UnitsManager instance;

    private UnitsManager() {
        Pool = new Dictionary<int, Unit>();
    }

     public static UnitsManager Instance {
        get {
            if (instance == null) {
                instance = new UnitsManager();
            }
            return instance;
        }
    }

    #endregion 

    private Dictionary<int, Unit> Pool;

    /// <summary>
    /// Check if an dead unit already exist, create one if not, update it if yes
    /// </summary>
    /// <param name="UD"></param>
    public void CreateUnit(UnitData UD) {
        Dictionary<int, Unit> deadUnits = Pool.Where(kv => kv.Value.IsDead() && (kv.Value.Type == UD.Type)).ToDictionary(kv => kv.Key, kv => kv.Value);
        Unit unit;

        if (deadUnits.Count > 0) {
            unit = deadUnits.First().Value;
            unit.Regenerate(UD);
        }
        else {
            unit = new Unit(UD);
        }

        Pool.Add(UD.Id, unit);
    }

    public void MoveUnit(int id, Vector2Int to) {
        Pool[id].Move(to);
    }

    public void MoveUnit(int id, Vector3 to) {
        Pool[id].Move(to);
    }

    public Unit GetUnitAtPos(Vector2Int pos) {
        Unit unit = null;

        foreach(KeyValuePair<int, Unit> Un in Pool) {
            if (Un.Value.position == pos && !Un.Value.IsDead()) {
                unit = Un.Value;
                break;
            }
        }

        return unit;
    }

}