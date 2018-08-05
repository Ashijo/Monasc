using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class MapManager {

    #region Singleton
    private static MapManager instance;

    private MapManager() { }

    public static MapManager Instance {
        get {
            if (instance == null)
                instance = new MapManager();

            return instance;
        }
    }
    #endregion

 
    /// <summary>
    /// Generate a new map
    /// The stored way to manage maps
    /// </summary>
    /// <param name="name">Must be unique, it is the ID</param>
    public void CreateNewMap(string name, Vector2Int size, GV.BATTLEFIELDTYPE bType) {
        Map map = new Map(name, size, bType);
        string jmap = JsonUtility.ToJson(map);
        string path = "Assets/Resources/BattlefieldMaps/" + map.GetName() + ".jmap";
        try {
            if (File.Exists(path)) {
                File.Delete(path);
            }
            File.WriteAllText(path, jmap);

        }
        catch (Exception e) {
            Debug.LogError(e.ToString());
        }
    }

    /// <summary>
    /// Get the names of all existings maps
    /// </summary>
    /// <returns>an array of maps names (Strings)</returns>
    public string[] GetAllMapsStr() {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/BattlefieldMaps");
        FileInfo[] infos = dir.GetFiles("*.jmap");
        string[] backer = new string[infos.Length];

        for (int i = 0; i < infos.Length; ++i) {
            string s = infos[i].Name;
            backer[i] = s.Remove(s.Length-5);
        }
        return backer;
    }


    /// <summary>
    /// Grab all existings maps
    /// </summary>
    /// <returns>array of all existing maps</returns>
    public Map[] GetAllMaps() {
        string[] mapStr = GetAllMapsStr();
        Map[] maps = new Map[mapStr.Length];

        for (int i = 0; i < mapStr.Length; ++i) {
            maps[i] = GetMap(mapStr[i]);
        }
        return maps;
    }


    /// <summary>
    /// Save the change on the map
    /// </summary>
    /// <param name="map">map to save</param>
    public void SaveMap(Map map) {

        string jmap = JsonUtility.ToJson(map);
        string path = "Assets/Resources/BattlefieldMaps/" + map.GetName() + ".jmap";
        try {
            if (File.Exists(path)) {
                File.Delete(path);
            }
            File.WriteAllText(path, jmap);

        }
        catch (Exception e) {
            Debug.LogError(e.ToString());
        }
    }


    /// <summary>
    /// Get a map by name
    /// </summary>
    /// <param name="name">name of the map you want get</param>
    /// <returns>the map found</returns>
    public Map GetMap(string name) {
        string path = "Assets/Resources/BattlefieldMaps/"+ name + ".jmap";
        string jmap = "";

        try {
            using (StreamReader sr = File.OpenText(path)) {
                string s;
                while ((s = sr.ReadLine()) != null) {
                    jmap += s;
                }
            }

        }
        catch (Exception e) {
            Debug.LogError(e.ToString());
        }

        Map map = JsonUtility.FromJson<Map>(jmap);
        return map;
    }


}
