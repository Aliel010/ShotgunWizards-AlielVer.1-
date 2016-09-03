using UnityEngine;
using System.Collections.Generic;

public class TargetTaggedTouching : Targeter, Triggers {

    public string targetTag;

    private List<GameObject> currentlyTargeting;

    void Start() {
        currentlyTargeting = new List<GameObject>();
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == targetTag) {
            currentlyTargeting.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == targetTag) {
            currentlyTargeting.Remove(other.gameObject);
        }
    }

    public override List<GameObject> GetTargets() {
        return currentlyTargeting;
    }
}
