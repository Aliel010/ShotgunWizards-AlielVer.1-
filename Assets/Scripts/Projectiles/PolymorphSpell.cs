using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolymorphSpell : MonoBehaviour {

    public GameObject sheepGO;
    private GameObject sheepPrefab;
    public GameObject enemyPrefab;
    private EnemyController enemyScript;

    private bool runOnce = true;
    private bool spellCast = false;
    private int enemyCounter = 0;

	// Use this for initialization
	void Start ()
    {
        enemyScript = enemyPrefab.GetComponent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && enemyCounter == 0)
        {
            enemyCounter++;
            Transform enemyPos;
            enemyPos = other.transform;//get position
            Destroy(other.gameObject); //destroy enemy

            sheepPrefab = (GameObject)Instantiate(sheepGO, enemyPos.position, enemyPos.rotation);  //spawn sheep
            TagChanger();
            sheepPrefab.gameObject.tag = "Player";
            //spellCast = true;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        print("Targeting closest player");
        enemyScript.TargetClosestPlayer();
    }

    void Update()
    {
        if (!sheepPrefab && runOnce)
        {
            TagRevert();
            runOnce = false;
        }

        if (spellCast)
        {
            StartCoroutine(Timer());
        }


    }

    void TagRevert()
    {
        //Change tag on sheep spawn
        //Find all the GO with tag player
        GameObject[] playerList;

        playerList = GameObject.FindGameObjectsWithTag("NotPlayer");

        //player tag becomes not player
        foreach (GameObject player in playerList)
        {
            player.tag = "Player";
        }
    }

    void TagChanger()
    {
        //Change tag on sheep spawn
        //Find all the GO with tag player
        GameObject[] playerList;

        playerList = GameObject.FindGameObjectsWithTag("Player");

        //player tag becomes not player
        foreach (GameObject player in playerList)
        {
            player.tag = "NotPlayer";
        }

        //When sheep dies player tag becomes player again
        
    }
}
