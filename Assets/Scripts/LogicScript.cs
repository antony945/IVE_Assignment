using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public static bool isPaused = false;
    public static bool isStopped = false;

    public void pauseGame() {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void resumeGame() {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void gameOver() {
        isStopped = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void restartGame() {
        isStopped = false;
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene("MyScene");
    }

    public void Update() {
        // Check pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");

            if (isPaused) {
                resumeGame();
            } else if (isStopped) {
                restartGame();
            } else {
                pauseGame();
            }
        }

        if (isStopped) {
            return;
        }

        // Check game over
        if (NPCController.POINTS < 0) {
            Debug.Log("GameOver");
            // Reset static variable for the score
            NPCController.POINTS = 0;   
            gameOver();
        }
    }
}
