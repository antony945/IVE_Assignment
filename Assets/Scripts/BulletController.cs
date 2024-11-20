using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public static int MAX_SCORE = 1000;
    private string logEntry = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after 3 seconds if it doesn't hit anything
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision) {
        // Check if the bullet hit an enemy or ally, and update the score accordingly.
        var whois = collision.gameObject.tag;

        if (whois != "Walls")
        {
            Debug.Log("HItted a object: " + whois);

            if (whois == "Enemy")
            { // If the bullet hits an enemy, handle that.
                HandleHit();
            }
            else if (whois == "Ally")
            { // If the bullet hits an ally, handle that.
                HandleMiss();
            }
            else
            { // If the bullet hits anything else, log it.
                Debug.Log("Unknown object: " + whois);
            }

            // Destroy collided object.
            Destroy(collision.gameObject);
        
            // Update UI
            NPCController.UpdateUI();
        }

        // Destroy the bullet.   
        Destroy(gameObject);
    }

    void HandleHit() {
        // Give score based on how much time it miss
        Scoring(Mathf.FloorToInt(TimerController.remainingTime/TimerController.DEFAULT_TIME * MAX_SCORE));
        NPCController.REMAINING_TO_HIT--;
    }

    void HandleMiss() {
        // Don't decrement score when miss
        // Scoring(-200);
        NPCController.CURRENT_MISS++;
    }

    void Scoring(int score) {
        // Update the global score and print it in the UI.
        NPCController.POINTS+=score;

        logEntry = $"Score (BulletController), {NPCController.POINTS} , {DateTime.Now.ToString()} \n";
        File.AppendAllText(NPCController.File_Path, logEntry);
    }

}
