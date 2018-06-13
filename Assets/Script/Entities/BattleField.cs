using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleField : MonoBehaviour {
    List<Case> battlefield;


	// Use this for initialization
	void Start () {
        gameObject.name = "BattleField";
	}

    public void GenerateBattlefield(Map map) {
        battlefield = new List<Case>();
        List<CaseData> cds = map.battleField;

        foreach (CaseData cd in cds) {
            Case c = new Case(cd.GetBType(), cd.GetCType(), cd.GetPosition(), cd.GetHeight());
            battlefield.Add(c);
        }
    }

	// Update is called once per frame
	void Update () {
		
	}


}
