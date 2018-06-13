using System.Collections;
using System.IO;
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


    GameObject battlefield;

 
    public void GenerateBattlefield(string name) {

        string json = File.ReadAllText("Assets/Resources/BattlefieldMaps/" + name + ".jmap");

        Map map = JsonUtility.FromJson<Map>(json);
        battlefield = new GameObject();
        battlefield.AddComponent<BattleField>();
        battlefield.GetComponent<BattleField>().GenerateBattlefield(map);

    }


}
