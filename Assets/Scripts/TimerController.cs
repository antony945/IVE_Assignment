using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] static public float remainingTime;
    static public float DEFAULT_TIME = 20f;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = DEFAULT_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        // Find the game object with a Timer tag, access to its text component and update the content.
        GameObject.FindWithTag("Timer").GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
