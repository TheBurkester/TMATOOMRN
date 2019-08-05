using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public AudioSource audioManager;
    public AudioClip shootSound;
    public AudioClip miss;
    public AudioClip hit;

    //how fast the shot travels when it is created
    [HideInInspector]
    public float shotSpeed;

    private void Start()
    {
        audioManager.clip = shootSound;
        audioManager.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //moves the  shot each frame
        transform.position += (transform.forward * shotSpeed * Time.deltaTime);
    }

    //destroys the shot upon colliding with something
    private void OnTriggerEnter(Collider other)
    {
        audioManager.clip = miss;
        audioManager.Play();
        AudioSource.PlayClipAtPoint(audioManager.clip, transform.position);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            audioManager.clip = hit;
        }
        else
        {
            audioManager.clip = miss;
        }
        AudioSource.PlayClipAtPoint(audioManager.clip, transform.position);
        Destroy(this.gameObject);
    }
}
