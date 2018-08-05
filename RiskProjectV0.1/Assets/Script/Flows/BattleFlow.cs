using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFlow : Flow {


    public override void InitializeFlow() {
<<<<<<< HEAD:RiskProjectV0.1/Assets/Script/Flows/BattleFlow.cs
        BattlefieldManager.Instance.GenerateBattlefield();
        
       
=======
        BattlefieldManager.Instance.GenerateBattlefield("testDesert");
>>>>>>> 17f7d762a1275c10cac26dc13c96c369f09f92bd:Assets/Script/Flows/BattleFlow.cs
    }

    public override void UpdateFlow(float _dt, InputParams _ip) {
        BattlefieldManager.Instance.Update(_ip);
    }
    public override void FixedUpdateFlow(float _fdt) { }
    public override void Finish() { }



}


