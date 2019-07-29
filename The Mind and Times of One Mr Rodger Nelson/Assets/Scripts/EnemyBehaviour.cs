using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    //elements relating to the enemy's AI
    private GameObject player;
    private NavMeshAgent agent;

    //values that track how how far the enemy can attack and see
    public float attackRange, sightRange;

    //a tracker for the enemey's lives
    public int lives = 3;

    //a boolean to track if the enemy is ranged or melee
    public bool ranged;

    //elements related to the enemy being ranged
    private float shotCounter = 0;
    public float shotMax = 2;
    private bool shotActive = true;
    public GameObject enemyShot;
    public Transform bulletSpawnPoint;
    public float shootingSpeed = 5;

    public AudioClip deathSound;
    private AudioSource audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = this.GetComponent<AudioSource>();
        //finds the player object and gets the enemy to move towards it when it sees it
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        //checks what attack type the enemy is

        //if the enemy is ranged, gives it higher attack range but lower sight range
        if (ranged == true)
        {
            attackRange = 1.5f;
            sightRange = 30f;
        }
        //else if the enemy is a melee enemy
        else
        {
            attackRange = 0.5f;
            sightRange = 35f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (Vector3.Distance(player.transform.position, transform.position))
        {
            case float i when i < attackRange:
                Attack();
                break;
            case float i when i < sightRange:
                MoveToPlayer();
                break;
            default:
                break;
        }

        if (lives <= 0)
        {
            die();
        }

        //Checks to see if the enemy has the ability to shoot
        //if they can't shoot on that frame
        if(ranged == true)
        {
            if (shotActive == false)
            {
                //increases the time on the shotCounter based on how much time has passed
                shotCounter += Time.deltaTime;
                //then, if a sufficient amount of time has passed
                if (shotCounter >= shotMax)
                {
                    //the player regains the ability to shoot, and the counter is reset
                    shotActive = true;
                    shotCounter = 0.0f;
                }
            }
        }
    }

    void Attack()
    {
       if(ranged == true && shotActive == true)
        {
            enemyShot.gameObject.GetComponent<BulletBehaviour>().shotSpeed = shootingSpeed;
            Instantiate(enemyShot, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            shotActive = false;
        }
        agent.ResetPath();

    }
        void MoveToPlayer()
    {
        if (!Physics.Linecast(player.transform.position, transform.position, out RaycastHit hitinfo, ~(1 << 9 | 1 << 10)))
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            lives -= 1;
            Debug.Log(lives);
            Destroy(other.gameObject);
        }
    }
    void die()
    {
        audioManager.clip = deathSound;
        audioManager.Play();
        this.gameObject.SetActive(false);
    }
}