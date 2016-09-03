using UnityEngine;
using System.Collections;

public class DestroyOnTime : MonoBehaviour {

    public float lifetime;
    private float timeOfDeath;

	// Use this for initialization
	void Start () {
        timeOfDeath = Time.time + lifetime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= timeOfDeath) {
            Destroy(gameObject);
        }
	}
}
