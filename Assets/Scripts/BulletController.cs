using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
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
            if (whois == "Enemy")
            { // If the bullet hits an enemy, add 1 to the score.
                Scoring(1);
            }
            else if (whois == "Ally")
            { // If the bullet hits an ally, subtract 1 to the score.
                Scoring(-1);
                // Destroy the object.
                Destroy(collision.gameObject);
            }
            else
            { // If the bullet hits anything else, log it.
                Debug.Log("Unknown object: " + whois);
            }

            // Destroy collided object.
            Destroy(collision.gameObject);
        }

        // Destroy the bullet.   
        Destroy(gameObject);
    }

    void Scoring(int score) {
        // Update the global score and print it in the UI.
        NPCController.POINTS+=score;
        // Find the game object with an UI tag, access to its text component and update the content.
        GameObject.FindWithTag("UI").GetComponent<Text>().text = "Score: " + (NPCController.POINTS);
    }

}
