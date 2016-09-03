using UnityEngine;
using System.Collections;

public class DestroyAfterFirstFrame : MonoBehaviour {

    private bool isFirst = false;
	
	// Update is called once per frame
	void Update () {
        if (isFirst) {
            isFirst = false;

        } else {
            Destroy(gameObject);
        }
	}
}
