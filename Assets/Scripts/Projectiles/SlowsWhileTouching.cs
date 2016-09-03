using UnityEngine;
using System.Collections.Generic;

public class SlowsWhileTouching : MonoBehaviour {

    public float slowAmount;

    private Targeter myTargeter;

    void Start() {
        myTargeter = GetComponent<Targeter>();
    }

	void OnTriggerEnter(Collider other) {
        StatsHandler otherStats = other.GetComponent<StatsHandler>();

        if (otherStats != null) {

            otherStats.moveSpeed -= slowAmount;
        }
    }

    void OnTriggerExit(Collider other) //increases everyone's speed with chain lightning
    {
        StatsHandler otherStats = other.gameObject.GetComponent<StatsHandler>();

        if (otherStats != null) {

            otherStats.moveSpeed += slowAmount;
        }
    }

    void OnDisable() {
        List<GameObject> targets = myTargeter.GetTargets();
        
        foreach (GameObject target in targets) {
            StatsHandler targetStats = target.GetComponent<StatsHandler>();

            if (targetStats != null) {

                targetStats.moveSpeed += slowAmount;
            }
        }
    }
}
