using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Case{

	public float Height             { get; protected set; }
    public GV.CASETYPES CaseType    { get; protected set; }
    public GameObject GameObject    { get; protected set; }
    public string StrCaseType       { get; protected set; }


    public Case(GV.BATTLEFIELDTYPE bType, GV.CASETYPES cType, Vector3 position, float height, GameObject parent) {

        #region DummyManager
        if (cType == GV.CASETYPES.DUMMY) {
            Debug.Log("Auto generated case");

            restart:
            switch (bType) {
                case GV.BATTLEFIELDTYPE.DUMMY:
                    Debug.LogError("Shouldn't be dummy here");
                    bType = Utils.Instance.GetRandomEnum<GV.BATTLEFIELDTYPE>(1);
                    goto restart;
                case GV.BATTLEFIELDTYPE.Desert:
                    cType = GenerateDesertCase();
                    break;
                case GV.BATTLEFIELDTYPE.Temper:
                    cType = GenerateMediumCase();
                    break;
                case GV.BATTLEFIELDTYPE.Ice:
                    cType = GenerateIcedCase();
                    break;
            }
        }
        #endregion

        Height = height * GV._PerCaseHeight;
        CaseType = cType;
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>(GV._PathToPrefabs + "BasicTile"));
        position.y = Height / 2;
        GameObject.transform.localPosition = position;
        GameObject.transform.localScale = new Vector3(1, Height, 1);
        if (cType == GV.CASETYPES.Cactus || cType == GV.CASETYPES.Fir || cType == GV.CASETYPES.Oak) {
            GenerateTree(cType.ToString());
            GameObject.GetComponent<Renderer>().material = Resources.Load<Material>(GV._PathToMaterials + "Cases/" + DefineSrtCaseType(cType));
        }
        else {
            GameObject.GetComponent<Renderer>().material = Resources.Load<Material>(GV._PathToMaterials + "Cases/" + cType.ToString());
        }
        GameObject.transform.SetParent(parent.transform);
        GameObject.name = "tile_" + cType.ToString();

    }


    #region CaseGenerators
    private GV.CASETYPES GenerateDesertCase() {
        GV.CASETYPES backer;
        float rand = Random.value;

        if (rand < GV._TreePortions) {
            backer = GV.CASETYPES.Cactus;
        }
        else if (rand < (.5f + GV._TreePortions / 2)) {
            backer = GV.CASETYPES.Sand;
        }
        else {
            backer = GV.CASETYPES.Oasis;
        }

        return backer;
    }
    private GV.CASETYPES GenerateIcedCase() {
        GV.CASETYPES backer;
        float rand = Random.value;

        if (rand < GV._TreePortions) {
            backer = GV.CASETYPES.Fir;
        }
        else if (rand < (.5f + GV._TreePortions / 2)) {
            backer = GV.CASETYPES.Snow;
        }
        else {
            backer = GV.CASETYPES.Ice;
        }

        return backer;
    }
    private GV.CASETYPES GenerateMediumCase() {
        GV.CASETYPES backer;
        float rand = Random.value;

        if (rand < GV._TreePortions) {
            backer = GV.CASETYPES.Oak;
        }
        else if (rand < (.5f + GV._TreePortions / 2)) {
            backer = GV.CASETYPES.Grass;
        }
        else {
            backer = GV.CASETYPES.River;
        }

        return backer;
    }
    #endregion

    private string DefineSrtCaseType(GV.CASETYPES caseType) {
        string backer = "";
        switch (caseType) {
            case GV.CASETYPES.Cactus:
                backer = "Sand";
                break;
            case GV.CASETYPES.Oak:
                backer = "Grass";
                break;
            case GV.CASETYPES.Fir:
                backer = "Snow";
                break;
            default:
                backer = "Canyon";
                break;
        }

        return backer;
    }


    private void GenerateTree(string treeName) {
        GameObject tree = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(GV._PathToPrefabs + "CasesDeco/" + treeName));
        tree.name = treeName;
        tree.transform.parent = GameObject.transform;
        Vector3 position = tree.transform.localPosition;
        position.x = 0;
        position.z = 0;
        position.y = position.y + GV._PerCaseHeight;
        tree.transform.localPosition = position;
    }
    

}
