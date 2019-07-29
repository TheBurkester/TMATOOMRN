using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //how fast the shot travels when it is created
    [HideInInspector]
    public float shotSpeed;
 

    // Update is called once per frame
    void Update()
    {
        //moves the  shot each frame
        transform.position += (transform.forward * shotSpeed * Time.deltaTime);
    }

    //destroys the shot upon colliding with something
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
