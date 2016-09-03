using UnityEngine;
using System.Collections;

public class AppliesElementalOnTouch<E> : MonoBehaviour where E : ElementalEffect {

    public void OnTriggerEnter(Collider other) {
        StatsHandler otherStats = other.gameObject.GetComponent<StatsHandler>();

        Debug.Log(otherStats);

        if(otherStats != null) {//if other object has stats
            otherStats.AddElementalEffect(other.gameObject.AddComponent<E>());
        }
    }
}
