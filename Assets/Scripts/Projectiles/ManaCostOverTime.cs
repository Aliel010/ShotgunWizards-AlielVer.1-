using UnityEngine;
using System.Collections;

public class ManaCostOverTime : MonoBehaviour {

    public float manaPerSec;

    private StatsHandler casterStats;

    void Start() {
        casterStats = gameObject.transform.parent.gameObject.GetComponent<StatsHandler>();
    }

    void Update() {//called when the component is disabled or set to be destroyed
        
        if(casterStats.GetMana() == 0) {
            Destroy(gameObject);
        }

        casterStats.ChangeMana(-manaPerSec * Time.deltaTime);
    }
}
