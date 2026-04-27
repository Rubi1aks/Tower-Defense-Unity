using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float panSpeed = 30;
    public float border = 20;

    bool doMovement = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement) return;
        if (Input.mousePosition.x > Screen.width - border || Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.mousePosition.x < border || Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }
        if (Input.mousePosition.y < border || Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y > Screen.height - border || Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * panSpeed/15, Space.Self);
    }
}
