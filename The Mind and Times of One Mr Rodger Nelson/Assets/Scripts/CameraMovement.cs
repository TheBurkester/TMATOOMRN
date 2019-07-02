using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Courtesy of James Leung
    public const float transitionSpeed = 0.3f;
    public GameObject destination;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Camera.main.transform.position, destination.transform.position) != 0)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, destination.transform.position, transitionSpeed);
        }
    }
}
