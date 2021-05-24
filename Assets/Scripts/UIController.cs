using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text speedText;
    public Text timeText;
    public Text ammoText;
    public Text levelText;
    public Text victoryText;
    public GameObject panel;

    public float time;
    public static float speed = 10;
    public static int ammo = 10;
    public static int health = 6;

    public Sprite healthEmpty;
    public Sprite health1;
    public Sprite health2;
    public Sprite health3;
    public Sprite health4;
    public Sprite health5;
    public Sprite healthFull;

    public AudioSource victory;

    public Image healthSprite;
    void Start()
    {
        victoryText.enabled = false;
        panel.SetActive(false);
        speed = 10;
        levelText.text = "Level " + GameManager.levelTrack;
        ammo = 10;
        health = 6;
    }

    void Update()
    {

        switch (health)
        {
            case 6:
                healthSprite.sprite = healthFull;
                break;
            case 5:
                healthSprite.sprite = health5;
                break;
            case 4:
                healthSprite.sprite = health4;
                break;
            case 3:
                healthSprite.sprite = health3;
                break;
            case 2:
                healthSprite.sprite = health2;
                break;
            case 1:
                healthSprite.sprite = health1;
                break;
            case 0:
                healthSprite.sprite = healthEmpty;
                break;
            default:
                healthSprite.sprite = healthEmpty;
                break;
        }
        time += Time.deltaTime;
        speedText.text = "Speed: " + speed + " MPH";
        timeText.text = "Time: " + time;
        ammoText.text = "Ammo: " + ammo;
        speed *= 1.002f;
        if (speed > 40)
        {
            speed = 40;
        }
        if(time > 88 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2DFrontScene"))
        {
            victoryText.enabled = true;
            panel.SetActive(true);
            victory.Play();
        }
        if (time > 90)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2DSideScene"))
                SceneManager.LoadScene("2DFrontScene");
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("2DFrontScene"))
            {
                GameManager.level += 0.25f;
                GameManager.levelTrack += 1f;
                if (GameManager.levelTrack == 4)
                    SceneManager.LoadScene("Credits");
                else
                    SceneManager.LoadScene("2DSideScene");
            }
        }
    }
}
