using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class will simply do lerp a gameobject
/// </summary>
public class ArrowLerper : MonoBehaviour {

    RectTransform RTransform;
    Vector3 CurrentPos;
    Vector3 GoalPos;
    Vector3 BeginPos;

    private bool switcher   = true;
    public bool goLeft      = true;
    public float intensity  = 5;
    public float lerpTime   = 1;
    private float currentLerpTime   = 0;

    // Use this for initialization
    void Awake () {

        RTransform = gameObject.GetComponent<RectTransform>();

        CurrentPos = RTransform.localPosition;
        GoalPos = RTransform.localPosition;
        BeginPos = RTransform.localPosition;

        if (goLeft) GoalPos.x -= intensity; else GoalPos.x += intensity;
    }



    // Update is called once per frame
    void Update () {
        CurrentPos = RTransform.localPosition;

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime) {
            currentLerpTime = lerpTime;
        }

        float perc = currentLerpTime / lerpTime;

        if (switcher) {
            CurrentPos.x = Mathf.Lerp(BeginPos.x, GoalPos.x, perc);
            if (CurrentPos.x.Near(GoalPos.x, 1f)) Switch();
        }
        else {
            CurrentPos.x = Mathf.Lerp(GoalPos.x, BeginPos.x, perc);
            if (CurrentPos.x.Near(BeginPos.x, 1f)) Switch();
        }

        gameObject.transform.localPosition = CurrentPos;
    }

    public void Switch() {
        currentLerpTime = 0f;
        switcher = !switcher;
    }

}
