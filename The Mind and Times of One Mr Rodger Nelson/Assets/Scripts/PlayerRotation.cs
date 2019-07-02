using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {


        //Following is Courtesy of James Leung
        //creates a ray that is to be projected onto the screen
        Ray orientRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //creates a plan for the ray to be cast onto
        Plane orientPlane = new Plane(Vector3.up, Vector3.zero);
        orientPlane.Translate(Vector3.down * transform.position.y);


        if (orientPlane.Raycast(orientRay, out float rayDistance))
        {
            //Creates a vector3 that dictates the point that the object will orient to
            Vector3 orientPoint = orientRay.GetPoint(rayDistance);
            Debug.DrawLine(Camera.main.transform.position, orientPoint, Color.red);
            //changes the rotation of the attached object to face the mouse
            gameObject.transform.LookAt(orientPoint);
        }
    }

}
