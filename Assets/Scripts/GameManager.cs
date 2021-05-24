using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
     Game Boundaries
         5.0f
          |
   -6.0f-----6.0f
          |
        -5.0f
     */

    //Enemies for the side facing scenes
    public GameObject fighterA;
    public GameObject mineA;
    public GameObject turretA;
    public GameObject suicideBomberA;
    public GameObject bomberA;

    //Enemies for the front facing scenes
    public GameObject fighterB;
    public GameObject mineB;
    public GameObject turretB;
    public GameObject suicideBomberB;
    public GameObject bomberB;
    public float xCoord = 8.0f;
    public float yCoord;
    public int currentScene;
    public int scene;
    public static float levelTrack = 1;
    public static float level = 1.0f;
    public static float previous = level;

    //timer variables
    public float mineConstant;
    public float fighterConstant;
    public float suicideConstant;
    public float turretConstant;
    public float bomberConstant;
    public float fighterTimer;
    public float mineTimer;
    public float turretTimer;
    public float suicideTimer;
    public float bomberTimer;
    public float increase;
    public float fighterMultiplier = 1;
    public float mineMultiplier = 1;
    public float suicideMultiplier = 1;
    public float turretMultiplier = 1;
    public float bomberMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        fighterConstant = 5.0f;
        fighterTimer = fighterConstant;
        mineConstant = 3.0f;
        mineTimer = mineConstant;
        suicideConstant = 9.0f;
        suicideTimer = suicideConstant;
        turretConstant = 11.0f;
        turretTimer = turretConstant;
        bomberConstant = 15.0f;
        bomberTimer = bomberConstant;
        increase = 0.01f;

        if (currentScene == 3)//if it is the menu screen
        {
            scene = 0;
        }
        else//if it is not the menu screen
        {
            if ((currentScene == 0) || (currentScene == 1))//(currentScene % 2) == 1)//spawn on side view
            {
                scene = 1;
                //InvokeRepeating("fighterSpawn", 5.0f, 5.0f);
                //InvokeRepeating("mineSpawn", 3.0f, 3.0f);
                //InvokeRepeating("turretSpawn", 7.0f, 7.0f);
                //InvokeRepeating("suicideSpawn", 9.0f, 9.0f);
                //InvokeRepeating("bomberSpawn", 11.0f, 25);
            }
            else//spawn on front view
            {
                scene = 2;
                //InvokeRepeating("fighterSpawn", 5.0f, 5.0f);
                //InvokeRepeating("mineSpawn", 3.0f, 3.0f);
                //InvokeRepeating("turretSpawn", 7.0f, 4.0f);
                //InvokeRepeating("suicideSpawn", 7.0f, 7.0f);
                //InvokeRepeating("bomberSpawn", 9.0f, 7.5f);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //reset the timers
        if (previous != level)
        {
            fighterTimer = fighterConstant;
            fighterMultiplier = 1;
            mineTimer = mineConstant;
            mineMultiplier = 1;
            suicideTimer = suicideConstant;
            suicideMultiplier = 1;
            bomberTimer = bomberConstant;
            bomberMultiplier = 1;
            turretTimer = turretConstant;
            turretMultiplier = 1;

            if (scene == 2)
            {
                suicideTimer -= 2.0f;
                mineTimer -= 1.0f;
                fighterTimer -= 1.5f;
            }

            previous = level;
        }

        fighterTimer -= increase * fighterMultiplier * (level);
        mineTimer -= increase * mineMultiplier * (level);
        suicideTimer -= increase * suicideMultiplier * (level);
        turretTimer -= increase * turretMultiplier * (level);
        bomberTimer -= increase * bomberMultiplier * (level);

        if ((scene == 1) && (Time.timeScale != 0))
        {
            //generate a random spawn coordinate for each spawn
            //spawn enemies
            xCoord = 8.0f;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //spawn for side scene
            if (fighterTimer <= 0)
            {
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(fighterA, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (fighterMultiplier < fighterConstant)
                {
                    fighterMultiplier = fighterMultiplier * 1.05f;
                }
                fighterTimer = fighterConstant;
            }
            if ((mineTimer <= 0) && (level != 1.0f))
            {
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(mineA, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (mineTimer < mineConstant)
                {
                    mineMultiplier = mineMultiplier * 1.05f;
                }
                mineTimer = mineConstant;
            }
            if (suicideTimer <= 0)
            {
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(suicideBomberA, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (suicideTimer < suicideConstant)
                {
                    suicideMultiplier = suicideMultiplier * 1.05f;
                }
                suicideTimer = suicideConstant;
            }
            if (turretTimer <= 0)
            {
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(turretA, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (turretTimer < turretConstant)
                {
                    turretMultiplier = turretMultiplier * 1.05f;
                }
                turretTimer = turretConstant;
            }
            if ((bomberTimer <= 0) && (level != 1.0f) && (GameObject.Find("bomberA(Clone)") == null))
            {
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(bomberA, new Vector3(8.0f, 4.4f), Quaternion.identity);
                //reset timer
                if (bomberTimer < bomberConstant)
                {
                    bomberMultiplier = bomberMultiplier * 1.05f;
                }
                bomberTimer = bomberConstant;
            }
        }//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //spawn for front scene
        else if(scene == 2)
        {
            //spawn enemies
            if (fighterTimer <= 0)
            {
                xCoord = Random.Range(-4.5f, 4.5f);
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(fighterB, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (fighterMultiplier < fighterConstant)
                {
                    fighterMultiplier = fighterMultiplier * 1.05f;
                }
                fighterTimer = fighterConstant;
            }
            if (mineTimer <= 0)
            {
                xCoord = Random.Range(-4.5f, 4.5f);
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(mineB, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (mineTimer < mineConstant)
                {
                    mineMultiplier = mineMultiplier * 1.05f;
                }
                mineTimer = mineConstant;
            }
            if (suicideTimer <= 0)
            {
                xCoord = Random.Range(-4.5f, 4.5f);
                yCoord = Random.Range(-4.5f, 4.5f);
                //spawn
                Instantiate(suicideBomberB, new Vector3(xCoord, yCoord), Quaternion.identity);
                //reset timer
                if (suicideTimer < suicideConstant)
                {
                    suicideMultiplier = suicideMultiplier * 1.05f;
                }
                suicideTimer = suicideConstant;
            }
        }
    }
}
