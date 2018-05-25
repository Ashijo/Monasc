using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldManager {

    #region Singleton
    private static BattlefieldManager instance;

    private BattlefieldManager() { }

    public static BattlefieldManager Instance {
        get {
            if (instance == null)
                instance = new BattlefieldManager();

            return instance;
        }
    }
    #endregion



    // Use this for initialization
    public void Start () {
	}

    public void CreateMap(string name = "Meadow") {
    }

	// Update is called once per frame
	void Update () {
		
	}
}
