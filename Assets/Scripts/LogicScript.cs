using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject winGameScreen;
    public static bool isPaused = false;
    public static bool isStopped = false;
    private string logEntry = string.Empty;

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

        logEntry = $"Final Result: , Score, {NPCController.POINTS}\n";
        logEntry = logEntry + $"***** Game Over (LogicScript), Game Over ,{System.DateTime.Now} , ***** \n";
        File.AppendAllText(NPCController.File_Path, logEntry);
    }

    public void winGame() {
        isStopped = true;
        Time.timeScale = 0f;
        winGameScreen.SetActive(true);
    }

    public void restartGame() {
        NPCController.POINTS = 0;
        TimerController.remainingTime = TimerController.DEFAULT_TIME;
        NPCController.CURRENT_MISS = 0;
        NPCController.REMAINING_TO_HIT = -1;
        isStopped = false;
        isPaused = false;
        Time.timeScale = 1f;
        winGameScreen.SetActive(false);
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
        if (TimerController.remainingTime <= 0 || NPCController.CURRENT_MISS == NPCController.MAX_MISS) {
            Debug.Log("GameOver");
            gameOver();
        }

        // Check win
        if (NPCController.REMAINING_TO_HIT == 0) {
            Debug.Log("Win");
            winGame();
        }
    }
}
