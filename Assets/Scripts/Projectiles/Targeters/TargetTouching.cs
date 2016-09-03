using UnityEngine;
using System.Collections.Generic;

public class TargetTouching : Targeter, Triggers {

    private List<GameObject> currentlyTargeting;

    void Start() {
        currentlyTargeting = new List<GameObject>();
    }

    public void OnTriggerEnter(Collider other) {
        currentlyTargeting.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other) {
        currentlyTargeting.Remove(other.gameObject);
    }

    public override List<GameObject> GetTargets() {
        return currentlyTargeting;
    }
}
