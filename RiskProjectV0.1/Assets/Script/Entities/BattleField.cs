using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {
    List<Case> battlefield;
    Map map;

	// Use this for initialization
	void Start () {
        gameObject.name = "BattleField";
	}

    public void GenerateBattlefield(Map map) {
        battlefield = new List<Case>();
        List<CaseData> cds = map.battleField;
        this.map = map;

        foreach (CaseData cd in cds) {
            Case c = new Case(cd.GetBType(), cd.GetCType(), cd.GetPosition(), cd.GetHeight(), gameObject);
            battlefield.Add(c);
        }

    }

    public Vector3 GetPos(Vector2Int MapPos) {
        try {
            return battlefield[MapPos.x + (MapPos.y * map.size.x)].GameObject.transform.localPosition;
        }
        catch {
            Debug.Log("it = " + MapPos.x + (MapPos.y * map.size.x));
            return battlefield[1 + (10 * map.size.x)].GameObject.transform.localPosition;
        }
    }

    public Vector2Int GetSize() {
        return map.size;
    }

    public Vector2Int GetMapPos(Vector3 pos) {
        Vector2Int backer = new Vector2Int();

        backer.x = Mathf.RoundToInt(pos.x);
        backer.y = Mathf.RoundToInt(pos.z) * -1;

        return backer;
    }

    public float GetHeight(Vector2Int MapPos) {
        return battlefield[MapPos.x + (MapPos.y * map.size.x)].Height;
    }

}
