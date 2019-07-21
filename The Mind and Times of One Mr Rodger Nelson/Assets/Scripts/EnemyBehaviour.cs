using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    public float attackRange = 1.5f, sightRange = 30f;

    public float maxHealth = 100, currentHealth;

    public Color maxHealthColor = Color.white, minHealthColor = Color.red;

    private float shotCounter = 0;
    public float shotMax = 2;
    private bool shotActive = true;
    public GameObject enemyShot;
    public Transform bulletSpawnPoint;
    public float shootingSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
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

        GetComponent<Renderer>().material.color = Color.Lerp(minHealthColor, maxHealthColor, currentHealth / maxHealth);
        if (currentHealth < 0)
        {
            gameObject.SetActive(false);
        }
        //Checks to see if the player has the ability to shoot
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
        if (shotActive == true)
        {
            enemyShot.gameObject.GetComponent<FreezingShot>().shotSpeed = shootingSpeed;
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
        if (other.gameObject.tag == "Shoot")
        {
            this.currentHealth -= 15;
        }
    }
}