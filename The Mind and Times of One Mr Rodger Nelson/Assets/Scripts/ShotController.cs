using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    //the object instance of the freezing shot
    public GameObject shotToShoot;
    //the place at which the freezing shot will spawn
    public Transform shotSpawner;

    //a bool to prevent the player from spamming the creation of freezing shots
    private bool shotActive = true;

    //a timer to determine how much time there is between the player being able to shoot freezing shots
    public float shotTimer = 1.0f;

    //a counter that counts towards the shot timer when the player cannot shoot
    public float shotCounter = 0.0f;
    //How fast the bullet travels when it is instantiated
    public float shootingSpeed;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    private int playerHealth = 3;

    private float invincibiliyTimer = 0.0f;
    private float invincibilityThreshold;
    private bool invincible = false;

    // Update is called once per frame
    void Update()
    {
        //Checks to see if the player has the ability to shoot
        //if they can't shoot on that frame
        if (shotActive == false)
        {
            //increases the time on the shotCounter based on how much time has passed
            shotCounter += Time.deltaTime;

            //then, if a sufficient amount of time has passed
            if (shotCounter >= shotTimer)
            {
                //the player regains the ability to shoot, and the counter is reset
                shotActive = true;
                shotCounter = 0.0f;
            }
        }

        //if they can shoot on that frame
        if (shotActive == true)
        {
            //if the left mouse button is being pressed
            if (Input.GetMouseButton(0))
            {
                //instantiates a freezing shot at the spawn point with the same rotation
                shotToShoot.gameObject.GetComponent<BulletBehaviour>().shotSpeed = shootingSpeed;
                Instantiate(shotToShoot, shotSpawner.position, shotSpawner.rotation); 

                //disables the player's ability to shoot to prevent spamming of bullets
                shotActive = false;
            }
        }
        //A timer used to track the player's invincibility
        if (invincible == true)
        {
            invincibiliyTimer += Time.deltaTime;
            if(invincibiliyTimer >= invincibilityThreshold)
            {
                invincible = false;
            }
        }
    }
    //If the player enters an enemy's attack, and they are not invincible
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Shot" && invincible == false)
        {
            //the Take Damage function is called
            takeDamage();
        }
    }

    //The take damage function, which is what tracks the player taking damage and the effects that occur
    void takeDamage()
    {
        playerHealth -= 1;
        if (playerHealth == 2)
        {
            life3.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            life2.SetActive(false);
        }
        else if (playerHealth == 0)
        {
            life1.SetActive(false);
            this.GetComponent<PlayerMovement1>().speed = 0;
        }
        //sets the invincible state to true, so that the player can't take too much damage in a short amount of time
        invincible = true;
    }

}
