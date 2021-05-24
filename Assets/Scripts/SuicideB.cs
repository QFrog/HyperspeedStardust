using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideB : MonoBehaviour
{
    public float xChange = .1f;
    public float yChange = .1f;
    public float zChange = .1f;
    public float increase = .01f;
    public int health = 2;
    public Vector2 destination;
    private Animator anim;
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject explosionPrefab;
    public GameObject player;//reference to the player
    public float speed = .05f;//speed of the enemy


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //set initial size to be small
        player = GameObject.Find("ShipB");
        transform.localScale = new Vector3(xChange, yChange, zChange);//update size
    }

    // Update is called once per frame
    void Update()
    {
        //if size is less than X, increase
        if (xChange < 1.25f)
        {
            transform.localScale = new Vector3(xChange, yChange, zChange);//update size
            xChange += increase;//update width
            yChange += increase;//update height
        }

        //move towards the player
        //update destination as players position
        destination = new Vector2(player.transform.position.x, player.transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }

    //detonate immediatly when the player touches it
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player") && (xChange >= 1.25f))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
            //Destroy(gameObject);
        }
        else if ((coll.gameObject.tag == "Laser"))
        {
            health--;
        }
        if(health <= 0)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
        }
    }
}
