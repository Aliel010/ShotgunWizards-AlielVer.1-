using UnityEngine;
using System.Collections;

public class SwitchTouchedCreature : MonoBehaviour {

    public GameObject switchTo;

    void OnTriggerEnter(Collider other) {

        //makes sure the other object is a creature and not a player
        if ((other.gameObject.GetComponent<StatsHandler>() != null) && (other.gameObject.tag != "Player")) {

            Instantiate(switchTo, other.gameObject.transform.position, other.gameObject.transform.rotation);

            Destroy(other.gameObject);
        }
        
    }
}
