using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    //public PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float spawnTime = 2f;
    public Transform spawnPoint;
    public int numOfEnemies = 3;
    public float waveWait = 5f;
    private bool respawnEnemies = true;
    private bool spawnEnemies = true;

    void Start() {
        StartCoroutine(Spawn());
    }
    

    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(spawnTime);
            int randNumber = Random.Range(0, enemy.Length);
            //if(playerHealth.currentHealth <= 0f)
            //{
            //return;
            //}


            //Allows spawning of only a set number of enemies (set by designer) in a random order from a list of enemies
            if ((GameObject.FindGameObjectsWithTag(enemy[0].tag).Length < numOfEnemies) && (spawnEnemies == true)) {
                Instantiate(enemy[randNumber], spawnPoint.position, spawnPoint.rotation);
            }

            //Stop spawns when max number is reached during wave
            if ((GameObject.FindGameObjectsWithTag(enemy[0].tag).Length == numOfEnemies)) {
                spawnEnemies = false;
            }

            //Next wave when enemy count hits 0
            if (GameObject.FindGameObjectsWithTag(enemy[0].tag).Length == 0) {
                //we can have an int waveNumber that increments and then we can increase numOfEnemies depending on that number
                //using if else statements.
                //numOfEnemies += 5; //(or whatever number you want to increment it by) Disabled for now - Scott
                yield return new WaitForSeconds(waveWait);
                spawnEnemies = true;
            }
        }

    }
}
