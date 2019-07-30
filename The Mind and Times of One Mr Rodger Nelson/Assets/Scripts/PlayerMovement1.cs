using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 movement = new Vector3(Input.GetAxisRaw("Vertical"), 0, -(Input.GetAxisRaw("Horizontal"))) * speed * Time.deltaTime;
        controller.Move(movement);
    }
}
