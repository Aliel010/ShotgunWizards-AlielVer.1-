using UnityEngine;
using System.Collections;

public class UntargetCenter : MonoBehaviour {

    public TargetTouching toUntarget;

    public void OnTriggerEnter(Collider other) {
        toUntarget.OnTriggerExit(other);
    }

    public void OnTriggerExit(Collider other) {
        toUntarget.OnTriggerEnter(other);
    }
}
