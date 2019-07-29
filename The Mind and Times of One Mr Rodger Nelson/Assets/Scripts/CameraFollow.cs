using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //Courtesy of James Leung

    public GameObject cameraPoint;
    private CameraMovement cameraScript;
    //whatever object the camera is set to follow
    //a temporary Vector3 that is updated each frame, rather than created
    private Vector3 tempVec3 = new Vector3();

    private void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraMovement>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraScript.destination = cameraPoint;
        }

    }
}
