using UnityEngine;

public class SpawnsArcOnDeath : MonoBehaviour {

    public GameObject toSpawn;
    public int amountToSpawn;
    public int arcAngle;
    public float arcRadius;
    public float randomVariance;
    
	
	// Called on disable or death
	void OnDisable () {
        float relAngle;
        float angleInc = arcAngle / (amountToSpawn - 1);
        for (float i = 0; i < amountToSpawn; ++i) {

            //get the amount the arc should varry by (+/-)
            float randomAngle = Random.value * randomVariance * 2 - randomVariance;
            
            relAngle = i * angleInc - arcAngle / 2.0f;
            GameObject spawned = (GameObject)Instantiate(toSpawn,
                                                          transform.position,
                                                          transform.rotation * Quaternion.Euler(0, relAngle + randomAngle, 0)
                                                          );
            //give spawned the same shooter as this
            //spawned.GetComponent<ProjectileController>().setShooter(GetComponent<ProjectileController>().getShooter());

            //move spawned object away from spawn point to form an arc
            spawned.transform.position += spawned.transform.forward * arcRadius;
        }
    }
}
