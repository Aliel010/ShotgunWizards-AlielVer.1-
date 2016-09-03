using UnityEngine;
using System.Collections;

public class FacesVelocity : MonoBehaviour {

    private Rigidbody myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //Rotates the object to "look" towards the velocity vector
        transform.rotation = Quaternion.LookRotation(
            myRigidbody.velocity,
            Vector3.forward
            );
	}
}
