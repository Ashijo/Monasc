using UnityEngine;
using System.Collections;


/// <summary>
/// The basic flow class, maybe I should switch for an interface
/// </summary>
public abstract class Flow {

    /// <summary>
    /// Everything inside will be call after the loading of the hierarchie
    /// </summary>
    public abstract void InitializeFlow();

    /// <summary>
    /// Must be call in an update unity
    /// </summary>
    /// <param name="_dt">the delta time</param>
    /// <param name="_ip">Input parametter</param>
    public abstract void UpdateFlow(float _dt, InputParams _ip);

    /// <summary>
    /// Must be call in an update unity
    /// </summary>
    /// <param name="_fdt">Fixed delta time</param>
    public abstract void FixedUpdateFlow(float _fdt);

    /// <summary>
    /// Every thing inside will be call before switching scene
    /// </summary>
    public abstract void Finish();

}
