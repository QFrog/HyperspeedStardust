using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public GameObject startButton;
    public GameObject tutButton;
    public GameObject startText;
    public GameObject tutText;
    
    public void startGame() {
        SceneManager.LoadScene("2DSideScene");
        GameManager.level = 1;
        GameManager.levelTrack = 1;
    }

    public void tutorial() {
        SceneManager.LoadScene("TutorialScene");
    }

    public void mainMenu() {
        SceneManager.LoadScene("StartScene");
    }
    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void quit()
    {
        Application.Quit();
    }

    public void StartButton() {
    startButton.SetActive(false);
    startText.SetActive(false);
    tutText.SetActive(true);
    tutButton.SetActive(true);
    }
}
