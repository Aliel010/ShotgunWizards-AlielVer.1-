using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Epifany: 

//BluePrint
/// <summary>
/// Code BluePrint:
/// Step 1: If we have any lightning chain uses left then, 
/// Step 2: Grow the size of this objects trigger collider(check)
/// Step 3: Add the enemies that have not been hit with the main lightning trigger to the new enemyHit list 
/// Step 4: increment the chain Usage 
/// Step 5: Go through enemyHit list and Spawn the subLightning gameObject on the enemies position
/// Step 6: parent the subLightning GameObject to the enemy.
/// 
/// What is not working?
/// Q1: 
///     
/// Q2: After the first lightning chain the enemy list is restarting
///     
/// Theory: The start function happens almost at the same time chain reaction is figuring out his list
/// </summary>
/// 

public class SubLightningSpell : MonoBehaviour
{
    //animation public variables
    public float timeScale = 1f;
    public float maxSize = 15;//default

    //Timer variables
    public float scanningTimer;

    //animation private variables
    private float initSize;
    private float progress = 0f;

    //Keeps track of enemies hit by sublightning
    [HideInInspector]
    public List<GameObject> enemiesHit = new List<GameObject>(); //already marked
    private List<GameObject> newEnemyHits = new List<GameObject>(); //enemies getting marked

    //Main Lightning Script
    public MainLightningSpell mainLightningScript;

    //variables for chainUsed and chainLenghts from Main lightning script
    [HideInInspector]
    public int chainUsage; //keeps track of chain usage
    private int maxChain; //limits how many lightnings you can chain

    //SubLightning Prefab original
    public GameObject subLightningPrefab;

    // Use this for initialization
    void Start()
    {
        initSize = GetComponent<SphereCollider>().radius; //initial size
        maxChain = mainLightningScript.chainLength;
        StartCoroutine(ScanningTimer(scanningTimer));
    }

    IEnumerator ScanningTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        ArcTriggers();
    }

    //goes one by one
    
    void OnTriggerEnter(Collider other)
    {
        if (chainUsage < maxChain) //check how many usages we have left
        {
            Debug.Log(other.gameObject.name);
            //if the second sphere hits an enemy that has not been hit by the main attack
            if (other.tag == "Enemy" && !enemiesHit.Contains(other.gameObject))
            {
                chainUsage++;
               
                Debug.Log("Enemy: "+ other.gameObject.name);
                newEnemyHits.Add(other.gameObject);//enemies about to be hit 
                enemiesHit.Add(other.gameObject); //enemies marked
            }
        }
    }

    void Update()
    {
        print("Growing....");
        //increase the sphere collider as long as there are still more chain usages
        if (chainUsage < mainLightningScript.chainLength)
        {
            SphereTriggerSizeAnimation();
        }
    }

    //Spawns the subLightning Spell
    void ArcTriggers()
    {
        foreach (GameObject targetHit in newEnemyHits)
        {
            GameObject subLightning = (GameObject)Instantiate(subLightningPrefab,
            targetHit.transform.position + Vector3.up,
            targetHit.transform.rotation); //instantiates the gameObject

            subLightning.GetComponent<SubLightningSpell>().chainUsage = chainUsage; //testing

            subLightning.transform.parent = targetHit.transform; //make sublightning a child of the enemy GO        
        }

        enemiesHit.Clear();
        newEnemyHits.Clear();
        chainUsage = 0;
    }

    //first animate the sphere to change sizes
    void SphereTriggerSizeAnimation()
    {

        if (progress < 1)
        {
            //print("StartTime: " + Time.time);
            this.GetComponent<SphereCollider>().radius = Mathf.Lerp(initSize, maxSize, progress);
            progress += Time.deltaTime * timeScale;
        }
        else
        {
            //print("finishTime: " + Time.time);
            progress = 1;
        }
    }
}
