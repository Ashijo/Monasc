using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Building {

    GameObject gameObject;

    public bool IsActive        { get; private set; }
    public BuildingType Type    { get; private set; }
    public int Team             { get; private set; }
    public Vector3 Position     { get; private set; }

    public Building(BuildingType type, int team) {
        IsActive = false;
        Type = type;
        Team = team;

        switch (Type) {
            case BuildingType.Casern:
                gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/macro/Casern"));
                break;
            default:
                Debug.LogError("Unmanage building type");
                break;
        }

    }
}

public enum BuildingType { Casern}
