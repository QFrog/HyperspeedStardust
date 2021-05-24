using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSideController : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject misslePrefab;
    public GameObject mainMenu;
    public GameObject tryAgain;
    public Text deadText;
    public bool hit = false;
    public GameObject panel;
    AudioSource damage;

    public float speed;
    public bool shot = false;

    private Rigidbody2D rb2d;

    void Start()
    {
        deadText.enabled = false;
        tryAgain.SetActive(false);
        mainMenu.SetActive(false);
        panel.SetActive(false);
        damage = GetComponent<AudioSource>();
        Time.timeScale = 1;
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        ShipFeedback();
        if (transform.position.y < -4.5)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }
        else if (transform.position.y > 4.5)
        {
            transform.position = new Vector3(transform.position.x, 4.5f, 0);
        }
        //Store the current horizontal input in the float moveHorizontal.
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(0, moveVertical);

        //if want floatly movement use the code below
        //rb2d.AddForce(movement * speed);

        //if want nonfloalty movement use the code below
        rb2d.velocity = movement * speed;
        ShootLaser();
        ShootMissile();
        shot = false;
    }
    void ShootLaser()
    {
        //adds laser shooting to the ship
        if ((Input.GetButtonDown("Fire")) && (shot != true))
        {
            //Debug.Log("laser fired");
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            laser.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
            Destroy(laser, 2.0f);
            shot = true;
        }
    }
    void ShootMissile()
    {
        //adds missile shooting the ship, limited ammo, fires faster than lasers
        if (Input.GetButtonDown("Fire2") && UIController.ammo > 0)
        {
            //Debug.Log("missile fired");
            GameObject missle = Instantiate(misslePrefab, transform.position, transform.rotation);
            missle.GetComponent<Rigidbody2D>().velocity = transform.right * 20f;
            Destroy(missle, 10.0f);
            UIController.ammo--;
        }
    }
    void ShipFeedback()
    {
        if (Input.GetButton("Vertical"))
        {
            transform.Rotate(0, 0, Input.GetAxis("Vertical") * 10 - transform.eulerAngles.z);
        }
        else
        {
            transform.Rotate(0, 0, Input.GetAxis("Vertical") * 0 - transform.eulerAngles.z);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Enemy") || (coll.gameObject.tag == "EnemyShot"))
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
    }
}