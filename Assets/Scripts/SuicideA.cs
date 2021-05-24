using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideA : MonoBehaviour
{
    //public Transform player;//the players current position
    public float speed = .035f;//speed of the enemy
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    public float waitTime = 2f;
    public bool shoot = true;
    //public Vector3 movement;
    public Vector2 destination;
    public int health = 2;
    private Animator anim;
    public GameObject explosionPrefab;
    private AudioSource deathSound;
    private AudioClip deathLength;
    private Animator animator;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        deathSound = GetComponent<AudioSource>();
        deathLength = deathSound.clip;
        animator = GetComponent<Animator>();
        destination = new Vector2(player.transform.position.x, player.transform.position.y);
        //target = transform.position = new Vector3(0.0f, 0.0f, 0.0f);

    }

    void FixedUpdate()
    {
        //update destination as players position
        destination = new Vector2(player.transform.position.x, player.transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        if (transform.position.x == -5.5f)
        {
            Destroy(gameObject);//destroy the game object
        }
        rotation();
    }

    //function to rotate towards the player
    public void rotation()
    {
        if ((transform.position.y > player.transform.position.y))// && (transform.rotation.eulerAngles.y > -25))//follows the player up
        {
            Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy eneds to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//25
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
        }
        if ((transform.position.y < player.transform.position.y))// && (transform.rotation.eulerAngles.y < 25))//follows the player
        {
            Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy eneds to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//-25
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
        }


    }

    /* public void FixedUpdate()
    {
        //rotate the enemy towards the player
        rotation();

    }*/

    void OnTriggerEnter2D(Collider2D coll)
    {
        //ignore collision if not player
        if (coll.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(coll.GetComponent<Collider2D>(), coll);
        }
        //if player destroy
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("suicide died from touching player");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5\
            Destroy(gameObject); //destories the original gameobject
        }
        //if player shot take damage
        if (coll.gameObject.tag == "Laser")
        {
            Destroy(coll.gameObject);
            health--;
        }
        //if player shot with missile instant destory
        if (coll.gameObject.tag == "Missile")
        {
            Destroy(coll.gameObject);
            health = 0;
        }
        //if health is below the threshold die
        if (health <= 0)
        {
            Debug.Log("suicide died from player");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5
            Destroy(gameObject); //destories the original gameobject
        }
    }
}