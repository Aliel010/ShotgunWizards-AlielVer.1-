using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainLightningSpell : MonoBehaviour {

    //Public variables
    public GameObject subLightning;//GameObject to instantiate

    //[HideInInspector]
    public int chainUsed; //keeps track of the lightning being chained
    public int chainLength = 3; //can be changed, It is the max ammount of lightning that can be chained

    //timer variables
    public float scanningTimer;

   // private Targeter myTargeter;

    //list of enemies marked
    [HideInInspector]
    public List<GameObject> targetsMarked = new List<GameObject>(); //Keeps track of who got hit by main lightning attack   

    // Initialization
    void Start()
    {
        chainUsed = 0;
        //myTargeter = gameObject.GetComponent<Targeter>();
        targetsMarked.Clear();

        StartCoroutine(ScanningTimer(scanningTimer));
    }

    //Trigger is activated first
    // Adds up to three enemy gameobject that got hit by main lightning
    void OnTriggerEnter(Collider other)
    {
        //if the gameObject's tag is the enemy
        StatsHandler otherStats = other.GetComponent<StatsHandler>();
        if (other.tag == "Enemy" && otherStats != null)
        {
            if (!targetsMarked.Contains(other.gameObject) && chainUsed < chainLength) // if there are not repeats of enemies and no more than 3 enemies
            {
                chainUsed++;
                targetsMarked.Add(other.gameObject); //adds enemy to the list
            }
        }
    }

    IEnumerator ScanningTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        SpawnTriggers();
    }

    void SpawnTriggers()
    {
        foreach (GameObject target in targetsMarked)
        {
            GameObject subLightningSpawn = (GameObject)Instantiate(subLightning,
                target.transform.position + Vector3.up,
                target.transform.rotation); //instantiates the gameObject on Enemy GO

            subLightningSpawn.GetComponent<SubLightningSpell>().chainUsage = chainUsed;
            subLightningSpawn.GetComponent<SubLightningSpell>().enemiesHit.Add(target);

            subLightningSpawn.transform.parent = target.transform; //makes sublightning a child of the enemy GO
        }
        targetsMarked.Clear();
    }
}


