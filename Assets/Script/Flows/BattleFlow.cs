using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFlow : Flow {


    public override void InitializeFlow() {
        BattlefieldManager.Instance.Start();
        BattlefieldManager.Instance.CreateMap();
    }
    public override void UpdateFlow(float _dt, InputParams _ip) {
    }
    public override void FixedUpdateFlow(float _fdt) { }
    public override void Finish() { }
}
