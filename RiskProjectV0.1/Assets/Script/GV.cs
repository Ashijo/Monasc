using System;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    #region Singleton
    private static GV instance;


    private GV() {

    }

    public static GV Instance {
        get {
            if (instance == null) {
                instance = new GV();
            }
            return instance;
        }
    }

    #endregion

    public static WS ws;

    // GLOBAL VARIABLES
    public enum SCENENAMES { DUMMY, MainMenu, MainScene, MainEntryScene, BattleScene, MacroScene }
    public static readonly string _PathToMaterials = "Material/";
    public static readonly string _PathToPrefabs = "Prefabs/";

    // BATTLE FIELD VARIABLES
    [System.Serializable]
    public enum CASETYPES { DUMMY, Rock, Canyon, Building, Sand, Cactus, Oasis, Snow, Fir, Ice, Grass, Oak, River }
    [System.Serializable]
    public enum BATTLEFIELDTYPE { DUMMY, Desert, Temper, Ice }
    public static readonly float _PerCaseHeight = 1f;
    public static readonly float _TreePortions = .10f;

    // IN GAME BATTLE VARIABLES
    public enum Units { Lancer = 0, Axeman = 1, Swordman = 2}
    public static readonly int[,] _StrenghTable = {{ 0,-1, 1},
                                                   { 1, 0,-1},
                                                   {-1, 1, 0}}; //_StrenghTable[Attacker, defender]

}