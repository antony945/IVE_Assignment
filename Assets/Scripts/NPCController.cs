using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class NPCController : MonoBehaviour
{
    public static int POINTS = 0; // Static variable to store the points
    public static int MAX_MISS = 3;
    public static int CURRENT_MISS = 0;
    public static int REMAINING_TO_HIT = -1;
    public static string File_Path = string.Empty;//Static variable to store the Path
    private string logEntry = string.Empty;
    public static int COUNT_SHOT = 0; // Static variable to store the count of shot

    // public float timeSpawn = 3f; // NPC spawn period
    // private float nextSpawn; // Time to spawn the next NPC
    // public float xRangeCoord = 20f; // Range of x coordinate to spawn the NPC

    [Range(0, 100)]
    public int enemyRate = 50; // Rate of enemy spawn
    public GameObject PrefabEnemy; // Prefab of the enemy NPC
    public GameObject PrefabAlly; // Prefab of the ally NPC

    [Range(1, 5)]
    public int HOW_MANY_TO_SPAWN = 5;

    void Start()
    {
        // Reset variable
        POINTS = 0;
        CURRENT_MISS = 0;
        REMAINING_TO_HIT = 0;
        COUNT_SHOT = 0;

        // // Set the time to spawn the next NPC
        // nextSpawn = Time.time + timeSpawn;
        // float xPos = UnityEngine.Random.Range(-xRangeCoord, xRangeCoord);
        float xPos = -20f;
        bool isEnemy = false;

        // At least one enemy in every stage
        int randomEnemyIndex = UnityEngine.Random.Range(0, HOW_MANY_TO_SPAWN);

        for (int i=0; i<HOW_MANY_TO_SPAWN; i++) {
            if (i==randomEnemyIndex) {
                isEnemy = true;
            } else {
                isEnemy = UnityEngine.Random.Range(0, 100) <= enemyRate;
            }

            if (SpawnAndDespawn(isEnemy, xPos)) {
                REMAINING_TO_HIT++;
            };
            xPos += 10;
        }

        createEventLog();
        NPCController.UpdateUI();
    }

    void Update() {
        if (LogicScript.isPaused || LogicScript.isStopped) {
            return;
        }

        // // Check if it is time to spawn the next NPC. If so, spawn the NPC and set the time to the next one
        // if(Time.time > nextSpawn) {
        //     nextSpawn = Time.time + timeSpawn;
        // }
    }

    public static void UpdateUI() {
        Debug.Log("Updated UI");

        // Find the game object with an UI tag, access to its text component and update the content.
        GameObject.FindWithTag("UI").GetComponent<Text>().text = "" + (NPCController.POINTS);
        GameObject.FindWithTag("Hit").GetComponent<Text>().text = (NPCController.REMAINING_TO_HIT) + " more target!";
        GameObject.FindWithTag("Miss").GetComponent<Text>().text = NPCController.CURRENT_MISS + "/" + NPCController.MAX_MISS + " miss";
    }

    public bool SpawnAndDespawn(bool isEnemy, float xPos){
        // set x position randomly for spawning the NPC and create the new location
        Vector3 newLocation = new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z);

        // Instantiate the NPC and destroy it after 3 seconds. Randomizing if it is an enemy or an ally
        GameObject NPC = null;        
        
        if (isEnemy) {
            NPC = Instantiate(PrefabEnemy, newLocation, Quaternion.identity);            
            logEntry = $"NPC Spawn (NPCController), Enemy,{System.DateTime.Now} \n";
        } else {
            NPC = Instantiate(PrefabAlly, newLocation, Quaternion.identity);
            logEntry = $"NPC Spawn (NPCController), Ally,{System.DateTime.Now} \n";
        } 
        
        Destroy(NPC, TimerController.DEFAULT_TIME);
        return isEnemy;
    }

    void createEventLog()
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName;
        string NameFile = String.Concat("EventsLog_", DateTime.Now.ToString("dd_MM_yyyy"), ".csv");
        File_Path = Path.Combine(projectPath, NameFile);

        // Si el archivo no existe, crea uno nuevo y pone un encabezado
        if (!File.Exists(File_Path))
        {
            File.WriteAllText(File_Path, " Game  starts at: " + System.DateTime.Now.ToString() + "  \n");
            File.AppendAllText(File_Path, "Event,Result,Datetime\n");

        }
    }
}
