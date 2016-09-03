using UnityEngine;
using System.Collections;

public class SpawnsOnDeath : MonoBehaviour {

    public GameObject toSpawn;
	
	void OnDisable () {
        //Spawn object if game still exists
        if (Application.isPlaying) {
            Instantiate(toSpawn, transform.position, transform.rotation);
        }
	}
}
