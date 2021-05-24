using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretA : MonoBehaviour
{
    //public Transform player;//the players current position
    public float speed = .01f;//speed of the enemy
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    public GameObject laserPrefab;
    public float waitTime = 4f;
    public bool shoot = false;
    //public Vector3 movement;
    public Vector2 destination;
    public int health = 4;
    private Animator anim;
    public GameObject explosionPrefab;
    public GameObject turretBase;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        destination = new Vector2(-7.0f, transform.position.y);
        //gameObject.transform.SetParent() =  Instantiate(turretBase, transform.position, Quaternion.identity);
        //target = transform.position = new Vector3(0.0f, 0.0f, 0.0f);

    }

    void Update()
    {
        //shoot a laser every few sec
        if (shoot == true)
        {
            //Debug.Log("laser fired");
            //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, transform.rotation.z + 90));
            //GameObject laser = Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, 0, transform.rotation.z + 90));
            //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.AngleAxis(90, Vector3.up));
            //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, -transform.rotation.z));// transform.rotation);
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            laser.GetComponent<Rigidbody2D>().velocity = transform.right * (-5f);

            Destroy(laser, 4.0f);

            shoot = false;

            StartCoroutine("waitShoot");
        }
        if (transform.position.x == -5.5f)
        {
            Destroy(gameObject);//destroy the game object
        }
    }

    //function to rotate towards the player
    public void rotation()
    {
        if ((transform.position.y > player.transform.position.y))// && (transform.rotation.eulerAngles.y > -25))//follows the player up
        {
            Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy needs to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 0, Vector3.forward);//25 135
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);

        }
        if ((transform.position.y < player.transform.position.y))// && (transform.rotation.eulerAngles.y < 25))//follows the player
        {
            Vector2 direction = -player.transform.position + transform.position;//find the direction that the enemy needs to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle + 0, Vector3.forward);//-25 135
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);

            //transform.position = new Vector3(0.0f, player.transform.position.y, 0.0f);//player.transform.position;//new Vector3(0.0f, 0.0f, 0.0f);
            //move = 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }

    //function to shoot a laser
    public IEnumerator waitShoot()
    //public void shoot()
    {
        yield return new WaitForSeconds(waitTime);//wait a little
        shoot = true;
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
        //if player destroy
        if (coll.gameObject.tag == "Player")
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
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
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
            //Destroy(gameObject);
        }
    }
}
