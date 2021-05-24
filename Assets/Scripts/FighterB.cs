using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterB : MonoBehaviour
{
    public float speed = .02f;//speed of the enemy
    private Animator anim;
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject explosionPrefab;
    public GameObject laserPrefab;//reference to the laser
    public Vector2 destination;//where it is heading
    public float waitTime = 4f;//how long to wait
    public bool shoot = true;//if it is good to shoot
    public bool change = true;
    public int health = 3;
    public float rando;
    public float xCoord;
    public float yCoord;
    public float xChange = .1f;//1.0f;
    public float yChange = .1f;//1.0f;
    public float zChange = .1f;//1.0f;
    public float increase = .01f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //change its initial size to be small
        transform.localScale = new Vector3(xChange, yChange, zChange);//update size

        //choose a random location
        //create the first random point to move towards
        xCoord = Random.Range(-6.0f, 6.0f);//generate a random num that is to the left of the current x-pos
        yCoord = Random.Range(-4.5f, 4.5f);
        destination = new Vector2(xCoord, yCoord);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if size is less than X, increase
        if (xChange < 1.5f)
        {
            transform.localScale = new Vector3(xChange, yChange, zChange);//update size
            xChange += increase;//update width
            yChange += increase;//update height
        }
        //dodge
        if((transform.position.x == xCoord) && (transform.position.y == yCoord))
        {
            //generate a new destination
            xCoord = Random.Range(-6.0f, 6.0f);//generate a random num that is to the left of the current x-pos
            yCoord = Random.Range(-4.5f, 4.5f);
            destination = new Vector2(xCoord, yCoord);
        }
        else
        {
            //head towards the destination
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
            //StartCoroutine("waitMove");
        }


        //shoot
        if ((shoot == true))
        {
            //Debug.Log("laser fired");
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
            //GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            //laser.GetComponent<Rigidbody2D>().velocity = transform.right * (-5f);

            //Destroy(laser, 4.0f);

            shoot = false;

            StartCoroutine("waitShoot");
        }
    }

    //function to wait to shoot a laser
    public IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(waitTime);//wait a little
        shoot = true;
    }

    //detonate immediatly when the player touches it
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Laser") && (xChange >= 1.25f))
        {
            health--;
            //Destroy(gameObject);
        }
        if(health <= 0)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
        }
    }
}
