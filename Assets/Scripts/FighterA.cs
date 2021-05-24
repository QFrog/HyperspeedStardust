using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterA : MonoBehaviour
{
    public float speed = .02f;//speed of the enemy
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    public GameObject laserPrefab;//reference to the laser
    public float waitTime = 6f;//how long to wait
    public bool shoot = true;//if it is good to shoot
    public Vector2 destination;//where it is heading
    public bool leave = false;//if it should leave
    public bool change = true;
    public float rando;
    public float xCoord;
    public float yCoord;
    public int health = 3;
    private Animator anim;
    public GameObject explosionPrefab;
    private AudioSource deathSound;
    private AudioClip deathLength;
    private Animator animator;

    /*
     Initial Spawn: (-7.0f,random)
     valid dimensions for movement
     Y - Coord: -4.5f - 4.5f
     X - Coord: -6.0f - 6.0f
     */
    //generate a random x for random movement
    //generate a random y for spawn and random movement

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        deathSound = GetComponent<AudioSource>();
        deathLength = deathSound.clip;
        animator = GetComponent<Animator>();
        destination = new Vector2(-7.0f, transform.position.y);//set initial destination in case random num freaks out
        //generate semi-random numbers for spawning
    }

    void Update()
    {
        //fly off the screen
        if (transform.position.x < (-3))
        {
            destination = new Vector2(transform.position.x, 5.5f);
            leave = true;
        }
        //generate random points to move towards for "dodging"
        else if(change == true)
        {
            xCoord = Random.Range(-6.0f, transform.position.x-1);//generate a random num that is to the left of the current x-pos
            yCoord = Random.Range(-4.5f, 4.5f);
            change = false;
            destination = new Vector2(-7.0f, yCoord);
            StartCoroutine("waitMove");
        }
        if (transform.position.y == 5.5f)
        {
            Destroy(gameObject);//destroy the game object
        }

        //shoot a laser every few sec
        if ((shoot == true) && (leave == false))
        {
            //Debug.Log("laser fired");
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            laser.GetComponent<Rigidbody2D>().velocity = transform.right * (-5f);

            Destroy(laser, 4.0f);

            shoot = false;

            StartCoroutine("waitShoot");
        }

        //shoot();
        //transform.Translate((Vector3.left * Time.deltaTime) / 2, Space.World);
    }

    /*
                 if ((transform.position.y > player.transform.position.y))// && (transform.rotation.eulerAngles.y > -25))//follows the player up
            {
                Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy eneds to face the player
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//25
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
            }
            if ((transform.position.y < player.transform.position.y))// && (transform.rotation.eulerAngles.y < 25))//follows the player
            {
                
                Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy needs to face the player
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//-25
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
            }
     */
    //function to rotate towards the player
    public void rotation()
    {
        if(leave == false){

            Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy needs to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//-25
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
        }
        else
        {
            Vector2 direction = new Vector2(transform.position.x, -5.5f);//tells the ship to turn up
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);//25
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }

    //function to wait to shoot a laser
    public IEnumerator waitShoot() 
    {
        yield return new WaitForSeconds(waitTime);//wait a little
        shoot = true;
    }

    //function to wait to dodge
    public IEnumerator waitMove()
    {
        yield return new WaitForSeconds(1.0f);//wait a little
        change = true;
    }

    public void FixedUpdate()
    {
        rotation();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //ignore collision if not player
        if (coll.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(coll.GetComponent<Collider2D>(), coll);
        }
        //if player destroy as failsafe
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("fighter died");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5
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
            //Destroy(gameObject);
            Debug.Log("fighter died");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5
            Destroy(gameObject); //destories the original gameobject
        }
    }
}
