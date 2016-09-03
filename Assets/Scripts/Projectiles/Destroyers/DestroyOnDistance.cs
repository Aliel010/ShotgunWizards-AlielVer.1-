using UnityEngine;
using System.Collections;

public class DestroyOnDistance : MonoBehaviour {
    //Assumes constant velocity

    public float distance;
    private float startTime;

    private Rigidbody myRigidbody;
	
	void Start () {
        startTime = Time.time;
        myRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        
        float timeOfDeath = startTime + distance / myRigidbody.velocity.magnitude;

        if (Time.time >= timeOfDeath) {
            Destroy(gameObject);
        }
	    
	}
}
