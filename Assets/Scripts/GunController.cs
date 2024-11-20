using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform bulletSpawnPoint; // Postion where the bullet will be spawned
    public GameObject bulletPrefab;
    public float bulletSpeed = 100.0f; // Speed factor of the bullet
    private string logEntry = string.Empty;
    void Update()
    {
        if (LogicScript.isPaused || LogicScript.isStopped) {
            return;
        }

        Shoot();
    }
    public void Shoot()
    {
        // if the player press the space bar or left click, then shoot
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(bulletPrefab); // Create a new bullet
            bullet.transform.position = bulletSpawnPoint.position; // Set the bullet position to the bulletSpawnPoint position
            // Shoot the bullet by adding velocity to the rigigbody component of the bullet
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

            NPCController.COUNT_SHOT += 1;
            logEntry = $"Count Shot (GunController),{NPCController.COUNT_SHOT}, {DateTime.Now.ToString()} \n";
            File.AppendAllText(NPCController.File_Path, logEntry);
        }
    }

}
