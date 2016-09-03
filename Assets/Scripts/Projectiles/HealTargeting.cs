using UnityEngine;
using System.Collections;

public class HealTargeting : MonoBehaviour {

    public float hps = 1;

    private Targeter myTargeter;

	// Use this for initialization
	void Start () {
        myTargeter = GetComponent<Targeter>();
	}
	
	// Update is called once per frame
	void Update () {
	    foreach(GameObject target in myTargeter.GetTargets()) {
            StatsHandler targetStats = target.GetComponent<StatsHandler>();

            if(targetStats != null) {
                targetStats.ChangeHealth(hps * Time.deltaTime);
            }
        }
	}
}
