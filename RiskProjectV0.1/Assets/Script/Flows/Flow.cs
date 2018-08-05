using UnityEngine;
using System.Collections;


/// <summary>
/// The basic flow class, maybe I should switch for an interface
/// </summary>
public abstract class Flow {

    public abstract void InitializeFlow();
    public abstract void UpdateFlow(float _dt, InputParams _ip);
    public abstract void FixedUpdateFlow(float _fdt);
    public abstract void Finish();

}
