using UnityEngine;
using System.Collections.Generic;

public class DamageOnDeath : MonoBehaviour {

    public float damageAmount;

    private Targeter targeter;

    void Start() {
        targeter = gameObject.GetComponent<Targeter>();
    }

    void OnDisable() {//called when the component is disabled or set to be destroyed

        //Get all targets
        List<GameObject> targets = targeter.GetTargets();

        foreach(GameObject target in targets) {

            if(target != null) {//if target still exists
                //if target has stats, apply damage
                StatsHandler targetStats = target.GetComponent<StatsHandler>();
                if (targetStats != null) {
                    targetStats.ChangeHealth(-damageAmount);
                }
            }
        }

    }
}
