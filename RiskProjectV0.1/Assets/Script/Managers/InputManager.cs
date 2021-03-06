﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    #region Singleton
    private static InputManager instance;


    private InputManager() {

    }

    public static InputManager Instance {
        get {
            if (instance == null) {
                instance = new InputManager();
            }
            return instance;
        }
    }

    #endregion


    private bool directionInUse = false;
    private bool selectorsInUse = false;


    // Update is called once per frame
    public InputParams Update() {
        Vector2Int direction = new Vector2Int();
        bool confirm = false;
        bool cancel = false;
        bool escape = false;
        bool one = false;
        bool two = false;
        bool three = false;


        if (Input.anyKeyDown) {
            directionInUse = false;
            //Debug.Log("anykey pressed");
            //Debug.Log("Horizontal = " + Input.GetAxis("Horizontal"));
            //Debug.Log("Vertical = " + Input.GetAxis("Vertical"));
        }

        if (Input.GetAxis("Horizontal") > .07) {
            //Debug.Log("pressed");

            if (!directionInUse) {
                direction.x++;
                //Debug.Log("Right pressed");
                directionInUse = true;
            }
        }
        if (Input.GetAxis("Horizontal") < -0.07) {
            //Debug.Log("pressed");
            if (!directionInUse) {
                direction.x--;
                //Debug.Log("Left pressed");
                directionInUse = true;
            }
        }
        if (Input.GetAxis("Vertical") > .07) {
            //Debug.Log("pressed");
            if (!directionInUse) {
                direction.y++;
                //Debug.Log("Up pressed");
                directionInUse = true;
            }
        }
        if (Input.GetAxis("Vertical") < -0.07) {
            //Debug.Log("pressed");
            if (!directionInUse) {
                direction.y--;
                //Debug.Log("Down pressed");
                directionInUse = true;
            }
        }

    

        if (Input.GetAxis("Submit") > .7) {
            if (!selectorsInUse) {
                confirm = true;
                //Debug.Log("Confirm pressed");
                selectorsInUse = true;
            }
        }
        if (Input.GetAxis("Cancel") > .7) {
            if (!selectorsInUse) {
                cancel = true;
                //Debug.Log("Cancel pressed");
                selectorsInUse = true;
            }
        }
        if (Input.GetAxis("Escape") > .7) {
            if (!selectorsInUse) {
                escape = true;
                //Debug.Log("Escape pressed");
                selectorsInUse = true;
            }
        }

        if (Input.GetAxis("One") > .7) {
            if (!selectorsInUse) {
                one = true;
                //Debug.Log("One pressed");
                selectorsInUse = true;
            }
        }
        if (Input.GetAxis("Two") > .7) {
            if (!selectorsInUse) {
                two = true;
                //Debug.Log("Two pressed");
                selectorsInUse = true;
            }
        }
        if (Input.GetAxis("Three") > .7) {
            if (!selectorsInUse) {
                three = true;
                //Debug.Log("3 pressed");
                selectorsInUse = true;
            }
        }

        if (Input.GetAxis("Submit") == 0 && Input.GetAxis("Cancel") == 0 && Input.GetAxis("Escape") == 0) {
            selectorsInUse = false;
        }

        return new InputParams(direction, confirm, cancel, escape, one, two, three);
	}

}


public class InputParams{
    public Vector2Int Direction { get; private set ;}
    public bool Confirm { get; private set; }
    public bool Cancel { get; private set; }
    public bool Escape { get; private set; }
    public bool Consumed { get; private set; }

    public bool onePressed { get; private set; }
    public bool twoPressed { get; private set; }
    public bool threePressed { get; private set; }

    public InputParams(Vector2Int direction, bool confirm, bool cancel, bool escape, bool one, bool two, bool three) {
        Direction = direction;
        Confirm = confirm;
        Cancel = cancel;
        Escape = escape;
        Consumed = false;
        onePressed = one;
        twoPressed = two;
        threePressed = three;

    }

    public void Use() {
        Consumed = true;
    }
}
