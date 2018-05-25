using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFlow : Flow {

    RectTransform selector;
    RectTransform[] options;
    int iterator;

    public override void InitializeFlow() {
        selector = GameObject.Find("Selector").GetComponent<RectTransform>();
        GameObject[] optGO = GameObject.FindGameObjectsWithTag("Option");
        options = new RectTransform[optGO.Length];
        for (int i = 0; i < optGO.Length; i++) {
            options[i] = optGO[i].GetComponent<RectTransform>();
        }
        iterator = 0;

    }

    public override void UpdateFlow(float _dt, InputParams _ip) {

        if (_ip.Direction.y < -0.7) {
            Down();
        } else if (_ip.Direction.y > 0.7) {
            Up();
        }

        if (_ip.Confirm) Select();


    }
    public override void FixedUpdateFlow(float _fdt) { }
    public override void Finish() { }

    private void Select() {
        switch (iterator) {
            case 0:
                FlowManager.Instance.ChangeFlows(GV.SCENENAMES.BattleScene);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

    }

    private void Down() {
        iterator = ClampIterator(--iterator);
        Vector3 optPosition = options[iterator].position;
        optPosition.x = selector.position.x;
        selector.position = optPosition;
    }
    private void Up() {
        iterator = ClampIterator(++iterator);
        Vector3 optPosition = options[iterator].position;
        optPosition.x = selector.position.x;
        selector.position = optPosition;
    }


    private int ClampIterator(int it) {
        if (it < 0) it = 3;
        if (it > 3) it = 0;
        return it;
    }


}
