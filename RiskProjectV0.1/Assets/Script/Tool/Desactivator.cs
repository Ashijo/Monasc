using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivator : MonoBehaviour {

    public void Desactivate() {
        gameObject.SetActive(false);
    }
}
