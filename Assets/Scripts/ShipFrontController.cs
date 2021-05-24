using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipFrontController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject tryAgain;
    public Text deadText;
    public GameObject panel;
    AudioSource damage;

    public float speed;//Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        damage = GetComponent<AudioSource>();
        panel.SetActive(false);
        deadText.enabled = false;
        tryAgain.SetActive(false);
        mainMenu.SetActive(false);
    }
    void Update()
    {
        ShipFeedback();
    }
    void FixedUpdate()
    {
        if (transform.position.y < -3.7)//bottom of screen
        {
            transform.position = new Vector3(transform.position.x, -3.7f, 0);
        }
        else if (transform.position.y > 5.7)//top of screen
        {
            transform.position = new Vector3(transform.position.x, 5.7f, 0);
        }
        if (transform.position.x < -6.15)//left of screen
        {
            transform.position = new Vector3(-6.15f, transform.position.y, 0);
        }
        else if (transform.position.x > 6.15)//right of screen
        {
            transform.position = new Vector3(6.15f, transform.position.y, 0);
        }
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //if want floatly movement use the code below
        //rb2d.AddForce(movement * speed);

        //if want nonfloalty movement use the code below
        rb2d.velocity = movement * speed;
    }
    void ShipFeedback()
    {
        //change for front view
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "EnemyShot"))
        {
            Destroy(coll.gameObject);
            UIController.health--;
            damage.Play();
        }
        if (UIController.health == 0)
        {
            //disabled for testing
            deadText.enabled = true;
            tryAgain.SetActive(true);
            mainMenu.SetActive(true);
            panel.SetActive(true);
            Time.timeScale = 0;
        }
        if (coll.gameObject.tag == "Suicide")//if player collided with a suicide bomber
        {
            SuicideB suicide = coll.gameObject.GetComponent<SuicideB>();
            if (suicide.xChange >= 1.0f)//if the enemy is of a specific size take damage
            {
                UIController.health--;
                damage.Play();
            }
        }
        if (coll.gameObject.tag == "Mine")//if player collided with a mine
        {
            MineB mine = coll.gameObject.GetComponent<MineB>();
            if (mine.xChange >= 1.5f)//if the enemy is of a specific size take damage
            {
                UIController.health--;
                damage.Play();
            }
        }
    }
}
