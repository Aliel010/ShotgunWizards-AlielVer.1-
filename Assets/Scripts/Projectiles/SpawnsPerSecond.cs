using UnityEngine;
using System.Collections;

public class SpawnsPerSecond : MonoBehaviour {

    public float timeBetweenSpawns;
    public GameObject toSpawn;

    public float angleVariance;

    private float nextSpawn;

	// Use this for initialization
	void Start () {
        nextSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(nextSpawn <= Time.time) {

            //get an angle of +/- angleVariance
            float randomAngle = Random.value * angleVariance * 2 - angleVariance;

            Instantiate(toSpawn, transform.position, transform.rotation * Quaternion.Euler(0, randomAngle, 0));

            nextSpawn += timeBetweenSpawns;
        }
	}
}
