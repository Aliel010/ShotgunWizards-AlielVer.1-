using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

    public int controllerNumber = 1;
    public float axisDeadzone = 0.3f;
    public float axisToButtonThreshold = 0.9f;

    public int moveXAxis = 1;
    public int moveYAxis = 2;
    public int lookXAxis = 4;
    public int lookYAxis = 5;
    public int primarySpellAxis = 3;//Trigger buttons are axis

    private int[] prevAxisButtonValue; 
    private int[] currentAxisButtonValue;
    private const int MAX_AXIS = 5;

    public int secondarySpellButton = 4;
    public int disarmButton = 5;
    public int fireButton = 0;
    public int startButton = 7;
    public int selectButton = 0;
    public int backButton = 1;

    void Start() {
        prevAxisButtonValue    = new int[5];
        currentAxisButtonValue = new int[5];
    }

    void Update() {
        for(int i = 0; i < MAX_AXIS; ++i) {
            prevAxisButtonValue[i] = currentAxisButtonValue[i];

            //i+1 restarts the loop with new index
            float axisValue = Input.GetAxis("Joystick " + controllerNumber + " Axis " + (i + 1)); //input from joystick controller
            
            if (axisValue > axisToButtonThreshold) { 
                currentAxisButtonValue[i] = 1;

            }
            else if (axisValue < -axisToButtonThreshold) { 
                currentAxisButtonValue[i] = -1;

            }
            else {
                currentAxisButtonValue[i] = 0; 
            }

        }
    }
    //Replacement Functions, use instead of normal input functions

    public bool GetButton(string button) {
        return Input.GetButton("Joystick " + controllerNumber + " Button " + StringToButtonNumber(button));
    }

    public bool GetButtonDown(string button) {
        return Input.GetButtonDown("Joystick " + controllerNumber + " Button " + StringToButtonNumber(button));
    }

    public bool GetButtonUp(string button) {
        return Input.GetButtonUp("Joystick " + controllerNumber + " Button " + StringToButtonNumber(button));
    }

    public float GetAxis(string axis) {
        return Input.GetAxis("Joystick " + controllerNumber + " Axis " + StringToAxisNumber(axis));
    }

    public float GetAxisRaw(string axis) {
        return Input.GetAxisRaw("Joystick " + controllerNumber + " Axis " + StringToAxisNumber(axis));
    }

    //Conversion functions

    public int GetAxisAsButton(string axis) {//For when two triggers are maped to 1 button. 1, 0, -1 will be outputed
        return currentAxisButtonValue[StringToAxisNumber(axis) - 1];
    }

    public int GetAxisAsButtonDown(string axis) {
        int axisNumber = StringToAxisNumber(axis);


        if (prevAxisButtonValue[axisNumber - 1] == 0) {
            return currentAxisButtonValue[axisNumber - 1];

        } else {
            return 0;
        }
    }

    public int GetAxisAsButtonUp(string axis) {
        int axisNumber = StringToAxisNumber(axis);

        if (currentAxisButtonValue[axisNumber - 1] == 0) {
            return prevAxisButtonValue[axisNumber - 1];

        }
        else {
            return 0;
        }
    }

    public bool GetAxisRawAsButton(string axis) {
        return GetAxisRaw(axis) > axisToButtonThreshold;
    }

    public float GetButtonAsAxis(string button) {

        if (GetButton(button)) {
            return 1.0f;
        } else {
            return 0.0f;
        }
    }

    //Functions for radial deadzoning

    public Vector3 GetMoveAxis() {
        Vector3 axisPosition = new Vector3(
            Input.GetAxisRaw("Joystick " + controllerNumber + " Axis " + moveXAxis),
            0.0f,
            Input.GetAxisRaw("Joystick " + controllerNumber + " Axis " + moveYAxis)
            );

        if (axisPosition.magnitude > axisDeadzone) {
            return axisPosition;
        }
        else {
            return new Vector3();
        }
    }

    public Vector3 GetLookAxis() {
        Vector3 axisPosition = new Vector3(
            Input.GetAxisRaw("Joystick " + controllerNumber + " Axis " + lookXAxis),
            0.0f,
            Input.GetAxisRaw("Joystick " + controllerNumber + " Axis " + lookYAxis)
            );

        if (axisPosition.magnitude > axisDeadzone) {
            return axisPosition;
        }
        else {
            return new Vector3();
        }
    }

    //Functions for use inside of this script only

    private int StringToAxisNumber(string s) {

        if (s == "Move X") {
            return moveXAxis;

        }
        else if (s == "Move Y") {
            return moveYAxis;

        }
        else if (s == "Look X") {
            return lookXAxis;

        }
        else if (s == "Look Y") {
            return lookYAxis;

        }
        else if (s == "Primary Spell") {
            return primarySpellAxis;

        }
        else {
            Debug.LogError("Axis name " + s + " was not recognised.");
            return 0;
        }
    }

    private int StringToButtonNumber(string s) {

        if (s == "Secondary Spell") {
            return secondarySpellButton;

        }
        else if (s == "Disarm") {
            return disarmButton;

        }
        else if (s == "Fire") {
            return fireButton;

        }
        else if (s == "Start") {
            return startButton;

        }
        else if (s == "Select") {
            return selectButton;

        }
        else if (s == "Back") {
            return backButton;

        }
        else {
            Debug.LogError("Button name " + s + " was not recognised.");
            return 0;
        }
    }
}


