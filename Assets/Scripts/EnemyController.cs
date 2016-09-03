using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    private GameObject targetPlayer;
    private StatsHandler playerStats;
    private NavMeshAgent nav;

    private StatsHandler myStats;

    public float attackCooldown = 1;
    public float attackDamage;
    public float attackRange;
    private float nextAttackAt;

    private bool firstUpdate = true;


    void Start() {
        
        TargetClosestPlayer();
        
        playerStats = targetPlayer.GetComponent<StatsHandler>();
        nav = GetComponent<NavMeshAgent>();

        myStats = GetComponent<StatsHandler>();

        nextAttackAt = 0;

        nav.speed = myStats.moveSpeed;
       
    }

    public void Init()
    {
        TargetClosestPlayer();

        playerStats = targetPlayer.GetComponent<StatsHandler>();
        nav = GetComponent<NavMeshAgent>();

        myStats = GetComponent<StatsHandler>();

        nextAttackAt = 0;

        nav.speed = myStats.moveSpeed;
    }

    public void TargetClosestPlayer() {
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");

        double targetDistance = Vector3.Distance(transform.position, playerList[0].transform.position);
        targetPlayer = playerList[0];

        foreach (GameObject player in playerList) {
            double thisDistance = Vector3.Distance(transform.position, player.transform.position);

            Debug.Log("List of players Targeted: " + player.name);

            if (thisDistance < targetDistance) //change
            {
                targetPlayer = player;
                targetDistance = thisDistance;
            }
        }
        Debug.Log("First on the list: " + targetPlayer);
    }

	
	// Update is called once per frame
	void Update () {
        //snyc speed
        nav.speed = myStats.moveSpeed;

        if (!myStats.IsAlive()) {
            Destroy(gameObject);
        }
        

        if( targetPlayer == null ) {
            TargetClosestPlayer();
        }

        nav.SetDestination(targetPlayer.transform.position);

        //attack if in range and cooldown has passed
        if (!firstUpdate && nav.remainingDistance < attackRange && Time.time > nextAttackAt) {
            targetPlayer.GetComponent<StatsHandler>().ChangeHealth(-attackDamage);

            //set when enemy can next attack
            nextAttackAt = Time.time + attackCooldown;
        }

        firstUpdate = false;
	}
}
