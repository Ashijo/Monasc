using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Map {
    public string name;
    public Vector2Int size;
    public GV.BATTLEFIELDTYPE type;
    public List<CaseData> battleField;

    public string GetName()                 { return name; }
    public List<CaseData> GetBf()           { return battleField; }
    public Vector2Int GetSize()             { return size; }
    public GV.BATTLEFIELDTYPE GetTypeBf()   { return type; }

    public Map(string name, Vector2Int size, GV.BATTLEFIELDTYPE type) {
        this.name = name;
        this.size = size;

        if (type == GV.BATTLEFIELDTYPE.DUMMY) {
            this.type = Utils.Instance.GetRandomEnum<GV.BATTLEFIELDTYPE>(1);
        }
        else {
            this.type = type;
        }

        this.battleField = new List<CaseData>();
        for (int i = 0; i < size.y; i++) {
            for (int j = 0; j < size.x; j++) {
                CaseData cs = new CaseData(type, GV.CASETYPES.DUMMY, new Vector3(j, 0, -i), 1);
                battleField.Add(cs);
            }
        }
    }

    public void UpdateCase(Vector2Int pos, GV.CASETYPES cType, float height) {
        battleField[pos.x + (pos.y * size.x)].Update(cType, height);
    }

    public float GetCaseHeight(Vector2Int pos) {
        return battleField[pos.x + (pos.y * size.x)].GetHeight();
    }

    public GV.CASETYPES GetCaseType(Vector2Int pos) {
        return battleField[pos.x + (pos.y * size.x)].GetCType();
    }

}
