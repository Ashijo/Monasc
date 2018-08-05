using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroSceneFlow : Flow {


    public override void Finish() {
    }

    public override void FixedUpdateFlow(float _fdt) {
    }

    public override void InitializeFlow() {
        MacroManager.Instance.Start();
       
    }

    public override void UpdateFlow(float _dt, InputParams _ip) {
        MacroManager.Instance.Update(_dt, _ip);
    }

}
