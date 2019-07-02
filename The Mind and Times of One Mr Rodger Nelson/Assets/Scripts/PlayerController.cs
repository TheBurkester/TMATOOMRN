using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //controls how fast the player moves sideways
    public float speed;

    //how high the player can jump
    public float jumpHeight;

    //a bool to check if the player has jumped already
    //prevents players from jumping in the air and from jumping off of walls
    [HideInInspector]
    public bool hasJumped = false;
    [HideInInspector]
    public bool rayCastTracker;

    // Update is called once per frame
    void Update()
    {
        //moves the player left if they are pressing the A key
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }

        //moves the player right if they are pressing the D key
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //makes the player jump when the w key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (hasJumped == false)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 100 * jumpHeight);
                hasJumped = true;
            }
        }

        //resets the player's rotation so that they may stand up if they fall over
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.identity;
        }

        //restarts the level when the player presses the R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartLevel();
        }

        //a raycast that is used to track if the player has landed on a platform
        if (rayCastTracker == true)
        {
            RaycastHit floorHit;

            //casts the ray just out of the bottom of the player
            float rayDistance = 1.0f;

            //if the raycast hits something
            if (Physics.Raycast(this.transform.position, Vector3.down, out floorHit, rayDistance) && hasJumped == true)
            {
                //the player's jump is reset
                hasJumped = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
        //occurs when player collides with a trigger
    {
        //if the player collides with the death plane
        if (other.tag == "Death")
        {
            restartLevel();       
        }

        //if the player collides with a falling platform that isn't frozen
        if (other.tag == "Platform")
        {
            restartLevel();        
        }

        //if the player collides with the end of the level
        if (other.tag == "LevelEnd")
        {
            restartLevel();
        }
    }

    //A function that restarts the level
    void restartLevel()
    {
        //resets the current scene by reloading it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
