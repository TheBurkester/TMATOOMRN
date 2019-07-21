using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingShot : MonoBehaviour
{
    //how fast the shot travels when it is created
    [HideInInspector]
    public float shotSpeed = 5;
 

    // Update is called once per frame
    void Update()
    {
        //moves the freezing shot each frame
        transform.position += (transform.forward * shotSpeed * Time.deltaTime);
    }

    //destroys the freezing shot upon colliding with something
    private void OnCollisionEnter(Collision collision)
    {
        
            Destroy(this.gameObject);
        
    }
}
