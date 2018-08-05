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

    public void Update(GV.CASETYPES cType, float height) {
        Vector3 position = GameObject.transform.position;
        GameObject.Destroy(GameObject);
        GameObject parent = GameObject.Find("BattleField");


        if (!parent) {
            parent = new GameObject();
            parent.name = "BattleField";
            parent.transform.position = new Vector3(0, 0, 0);
        }


        Height = height * GV._PerCaseHeight;
        CaseType = cType;
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>(GV._PathToPrefabs + "BasicTile"));
        position.y = Height / 2;
        GameObject.transform.position = position;
        GameObject.transform.localScale = new Vector3(1, Height, 1);
        GameObject.GetComponent<Renderer>().material = Resources.Load<Material>(GV._PathToMaterials + "Cases/" + cType.ToString());
        GameObject.transform.SetParent(parent.transform);
        GameObject.name = "tile_" + cType.ToString();
    }
    // TODO CLEAN

    public Case(float height, GV.CASETYPES caseType, Vector3 position, GameObject parent) {
        Height = height * GV._PerCaseHeight;
        CaseType = caseType;
        StrCaseType = CaseType.ToString();
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>(GV._PathToPrefabs + "BasicTile"));
        position.y += Height / 2;
        GameObject.transform.position = position;
        GameObject.transform.localScale = new Vector3(1,Height,1);
        GameObject.GetComponent<Renderer>().material = Resources.Load<Material>(GV._PathToMaterials + "Cases/" + StrCaseType);
        GameObject.transform.SetParent(parent.transform);
        GameObject.name = "tile_" + StrCaseType;
    }

    /*
    public Case(float height, byte battlefieldType, Vector3 position, GameObject parent) {
        Height = height * GV._PerCaseHeight;

        CaseType = SelectCase(battlefieldType);
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>(GV._PathToPrefabs + "BasicTile"));
        position.y += Height / 2;
        GameObject.transform.position = position;
        GameObject.transform.localScale = new Vector3(1, Height, 1);
        StrCaseType = DefineSrtCaseType(CaseType, battlefieldType);
        GameObject.GetComponent<Renderer>().material = Resources.Load<Material>(GV._PathToMaterials + "Cases/" + StrCaseType);
        GameObject.transform.SetParent(parent.transform);
        GameObject.name = "tile_" + CaseType.ToString();
    }*/

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

    private GV.CASETYPES SelectCase(byte battlefieldType) {
        GV.CASETYPES backer = GV.CASETYPES.River;

        switch (battlefieldType) {
            //Desert
            case 0:
                backer = GenerateDesertCase();
                break;
            case 1:
                backer = GenerateMediumCase();
                break;
            case 2:
                backer = GenerateIcedCase();
                break;
            default:
                break;
        }
        
        return backer;
    }

    private void GenerateTree(byte battlefieldType) {
        string treeName = "";
        switch (battlefieldType) {
            case 0:
                treeName = "Cactus";
                break;
            case 1:
                treeName = "Oak";
                break;
            case 2:
                treeName = "Fir";
                break;

        }
        GameObject tree = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(GV._PathToPrefabs + "CasesDeco/" + treeName));
        tree.name = treeName;
        tree.transform.parent = GameObject.transform;
        Vector3 position = tree.transform.localPosition;
        position.x = 0;
        position.z = 0;
        position.y = position.y + GV._PerCaseHeight;
        tree.transform.localPosition = position;

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
