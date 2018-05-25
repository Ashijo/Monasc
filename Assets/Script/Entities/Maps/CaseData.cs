using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaseData {

    [SerializeField]
    private int bType; // GV.BATTLEFIELDTYPE
    [SerializeField]
    private int cType; // GV.CASETYPES
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private float height;

    public GV.BATTLEFIELDTYPE GetBType() { return (GV.BATTLEFIELDTYPE) bType; }
    public GV.CASETYPES GetCType() { return (GV.CASETYPES) cType; }
    public Vector3 GetPosition() { return position; }
    public float GetHeight() { return height; }

    public CaseData(GV.BATTLEFIELDTYPE bType, GV.CASETYPES cType, Vector3 position, float height) {
        this.bType = (int)bType;
        this.cType = (int)cType;
        this.position = position;
        this.height = height;
    }

    public void Update(GV.CASETYPES cType, float height) {
        this.cType = (int)cType;
        this.height = height;
    }

}
