using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntryFlow : Flow {
    public override void Finish() {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdateFlow(float _fdt) {
        throw new System.NotImplementedException();
    }

    public override void InitializeFlow() {
        Debug.Log("MainEntryFlow Init");

        FlowManager.Instance.ChangeFlows(GV.SCENENAMES.MainMenu);
    }

    public override void UpdateFlow(float _dt, InputParams _ip) {
        throw new System.NotImplementedException();
    }
}
