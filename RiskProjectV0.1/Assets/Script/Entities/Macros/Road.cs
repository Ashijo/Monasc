using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    [SerializeField]
    private GameObject PointA;
    [SerializeField]
    private GameObject PointB;

    public GameObject GetA() {
        return PointA;
    }

    public GameObject GetB() {
        return PointB;
    }


    /*  A try but angle didn't work well :/
     *  
    public void Start() {

        Vector3 APos = PointA.transform.position;
        Vector3 BPos = PointB.transform.position;

        float distance = Vector3.Distance(APos, BPos);
        float angle = Vector3.Angle(APos, BPos);

        Vector3 pos = (APos + BPos) / 2;
        pos.y = .46f;

        gameObject.transform.position = pos;
        gameObject.transform.Rotate(new Vector3(0, angle, 0));
        gameObject.transform.localScale = new Vector3(distance / 10, 0.1f, 0.05f);


        Debug.Log(distance);
        Debug.Log(angle);

    }*/

}
