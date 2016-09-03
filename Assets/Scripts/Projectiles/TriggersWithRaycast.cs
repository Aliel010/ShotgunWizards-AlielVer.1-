using UnityEngine;
using System.Collections;

public class TriggersWithRaycast : MonoBehaviour {

    private Rigidbody myRigidbody;

    void Start() {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
        Triggers[] triggers = gameObject.GetComponents<Triggers>();

        RaycastHit hit;
        if (myRigidbody.SweepTest(transform.up, out hit, myRigidbody.velocity.magnitude * Time.deltaTime)) {

            foreach (Triggers trigger in triggers) {
                trigger.OnTriggerEnter(hit.collider);
            }
        }
    }
}
