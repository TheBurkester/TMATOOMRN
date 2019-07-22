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
    private float shotCounter = 0.0f;
    //How fast the bullet travels when it is instantiated
    public float shootingSpeed;



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
    }
}
