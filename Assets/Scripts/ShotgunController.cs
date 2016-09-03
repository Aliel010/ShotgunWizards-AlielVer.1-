using UnityEngine;
using System.Collections;

public class ShotgunController : MonoBehaviour {

    public float timeBetweenShots = 1.0f;
    private float nextFire;

    public float initalMove;//The distance to move the shot to get it out of the shotgun's collider

    public GameObject bullet;

	void Start() {
        nextFire = 0;
    }

    public void fire() {
        if(Time.time > nextFire) {
            nextFire = Time.time + timeBetweenShots;

            GameObject firedBullet = (GameObject) Instantiate(bullet,
                                                              transform.position,
                                                              transform.rotation * Quaternion.Euler(0, 0, 0)
                                                              );

            //Move the fired bullet out of the shotgun
            firedBullet.transform.position += firedBullet.transform.forward * initalMove;
        }
        

    }
}
