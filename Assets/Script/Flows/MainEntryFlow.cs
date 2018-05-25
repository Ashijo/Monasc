using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntryFlow : Flow {

    public override void InitializeFlow() {
        Debug.Log("MainEntryFlow Init");

        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.MainMenu);
    }
}
