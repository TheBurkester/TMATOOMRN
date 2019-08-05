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

    //elements related to the enemy attacking
    private float shotCounter = 0;
    public float shotMax = 1.5f;
    private bool shotActive = true;
    public GameObject enemyShot;
    public Transform bulletSpawnPoint;
    public float shootingSpeed = 5;

    private bool dead = false;

    public AudioClip deathSound;

    private int audioCounter;

    private float activeCounter;
    private float activeMax;

    private AudioSource audioManager;

    public List<AudioClip> audioList;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = this.GetComponent<AudioSource>();
        //finds the player object and gets the enemy to move towards it when it sees it
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
      
            attackRange = 4.0f;
            sightRange = 30f;
        activeCounter = Random.Range(0.0f, 1.5f);
        activeMax = Random.Range(6.0f, 10.0f);
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

        //the instance that the enemy dies
        if (lives <= 0)
        {
            audioManager.clip = deathSound;
            AudioSource.PlayClipAtPoint(audioManager.clip, transform.position);
            Destroy(this.gameObject);
        }

        if (activeCounter <= activeMax)
        {
            activeCounter += Time.deltaTime;
        }
        if (activeCounter >= activeMax)
        {
            int soundByte = Random.Range(0, 3);
            audioManager.clip = audioList[soundByte];
            AudioSource.PlayClipAtPoint(audioList[soundByte], transform.position);
            activeCounter = Random.Range(0.0f, 1.5f);
        }

        //Checks to see if the enemy has the ability to shoot
        //if they can't shoot on that frame
        
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

    void Attack()
    {
        //if the enemy has the ability to shoot and it's not dead
       if(shotActive == true)
        {
            //sets the shot speed of the bullet prefab to match the speed attached to the enemy
            enemyShot.gameObject.GetComponent<BulletBehaviour>().shotSpeed = shootingSpeed;
            //creates a bullet
            Instantiate(enemyShot, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            shotActive = false;
        }
       //resets the enemy's path
        agent.ResetPath();

    }
    //moves the enemy towards the player
        void MoveToPlayer()
    {
        if (!Physics.Linecast(player.transform.position, transform.position, out RaycastHit hitinfo, ~(1 << 9 | 1 << 10)))
        {
            agent.SetDestination(player.transform.position);
        }
    }

    //Occurs when a bullet from the player collides with the enemy
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            lives -= 1;
            Debug.Log(lives);
            Destroy(other.gameObject);
        }
    }
  
}