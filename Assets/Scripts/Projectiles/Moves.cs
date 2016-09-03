using UnityEngine;
using System.Collections;

public class Moves : MonoBehaviour {

    public float speed;
    public Vector3 directionDegrees;

    // Use this for initialization
    void Start()
    {

        //move the object relitive to forward rotation
        transform.Rotate(directionDegrees);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
