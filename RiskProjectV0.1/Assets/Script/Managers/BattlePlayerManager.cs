using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerManager{

    #region Singleton
    private static BattlePlayerManager instance;

    private BattlePlayerManager() { }

    public static BattlePlayerManager Instance {
        get {
            if (instance == null)
                instance = new BattlePlayerManager();

            return instance;
        }
    }
    #endregion



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
