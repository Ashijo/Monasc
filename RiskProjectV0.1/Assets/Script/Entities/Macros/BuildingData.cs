using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData {

    public bool IsActive { get; private set; }
    public BuildingType Type { get; private set; }
    public int Team { get; private set; }
    public Vector3 Position { get; private set; }

}
