using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberA : MonoBehaviour
{
    private Rigidbody2D rb2d;//rigidbody2D
    private Animator anim;
    public GameObject explosionPrefab;
    public GameObject player;//reference to the player
    public GameObject laserPrefab;//reference to the laser
    public float speed = .01f;//speed of the enemy
    public float xCoord = -4.0f;
    public float yCoord = 4.4f;
    public Vector2 destination;
    public int health = 6;
    public int shotNum = 0;
    public bool shoot;
    public int phase = 0;
    public float fire = -3.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        destination = new Vector2(xCoord, yCoord);
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {

        //fire shotgun burst
        if ((transform.position.x <= (fire)) && (shoot != true) && (phase == 0))//if at the given location
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            shotNum += 1;
            //Debug.Log(shotNum);
            shoot = true;
        }
        if ((transform.position.x <= (fire)) && (shoot != true) && (phase == 1))//if at the given location
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            shotNum += 1;
            //Debug.Log(shotNum);
            shoot = true;
        }
        if ((transform.position.x <= (fire)) && (shoot != true) && (phase == 2))//if at the given location
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            shotNum += 1;
            //Debug.Log(shotNum);
            shoot = true;
        }
        if ((transform.position.x <= (fire)) && (shoot != true) && (phase == 3))//if at the given location
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            shotNum += 1;
            //Debug.Log(shotNum);
            shoot = true;
        }
        if ((transform.position.x <= (fire)) && (shoot != true) && (phase == 4))//if at the given location
        {
            Instantiate(laserPrefab, transform.position, transform.rotation);
            shotNum += 1;
            //Debug.Log(shotNum);

            //leave
            destination = new Vector2(-6.0f, 4.4f);
            shoot = true;
        }

        if (shoot == true)
        {
            phase++;
            shoot = false;
        }

        //move
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        //destroy once it has gone off screen
        if (transform.position.x <= -6.0f)
        {
            Destroy(gameObject);
        }
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
