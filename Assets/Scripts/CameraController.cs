using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 rotation; // vector for manage rotation
    public float speedRotation = 200.0f; // speed factor for rotation
    void Start()
    {
        // Lock the cursor at the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Is stopped?" + LogicScript.isStopped);

        if (LogicScript.isPaused || LogicScript.isStopped) {
            Debug.Log("Unlocked cursor");
            Cursor.lockState = CursorLockMode.None;
            return;
        } else {
            Debug.Log("Locked cursor");
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Get the mouse movement and set the rotation vector
        rotation.x += Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime;
        rotation.y += Input.GetAxis("Mouse Y")  * speedRotation * Time.deltaTime;
        // Set the rotation of the camera with the new vector
        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }
}
