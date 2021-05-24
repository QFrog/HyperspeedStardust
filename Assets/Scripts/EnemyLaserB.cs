using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserB : MonoBehaviour
{
    public float xChange = 1.0f;
    public float yChange = 1.0f;
    public float zChange = 1.0f;
    public float increase = .01f;
    public Vector2 destination;
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    private Collider2D collider;
    public float speed = .05f;//speed of the enemy
    public float xCoord = 0;
    public float yCoord = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("ShipB");
        collider = GetComponent<Collider2D>();
        xCoord = player.transform.position.x;
        yCoord = player.transform.position.y;
        destination = new Vector2(xCoord, yCoord);
        //destination = new Vector2(player.transform.position.x, player.transform.position.y);
        //update destination as players position
        //destination = player.transform.position;// new Vector2(xCoord, yCoord);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.position.x == xCoord)
        {
            destination = new Vector2(xCoord * 2, yCoord);
        }
        if(transform.position.y == yCoord)
        {
            destination = new Vector2(xCoord, yCoord * 2);
        }
        */
        //approach screen
        if (xChange < 1.0f)
        {
            transform.localScale = new Vector3(xChange, yChange, zChange);//update size
            xChange += increase;//update width
            yChange += increase;//update height
        }
        else
        {
            collider.enabled = !collider.enabled;
        }

        //destination = new Vector2(0, 0);

        //move towards the players previous location
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        if ((transform.position.x < -6.0f) || (transform.position.x > 6.0f) || (transform.position.y < -5.0f) || (transform.position.y > 5.0f))
        {
            Destroy(gameObject);
        }

        //if size is big enough and it has reached the players previous location then destroy the bullet after a short delay
        if (((transform.position.x == xCoord) && (transform.position.y == yCoord)) && (xChange >= 1.0f))
        {
            StartCoroutine("wait");//destroy bullet if it lingers long enough
        }
    }

    //detonate immediatly when the player touches it
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player") && (xChange >= 1.0f))
        {
            Destroy(gameObject);//destroy the game object if player touches it
        }
    }

    //destroy it after a short time
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(2.0f);//wait a little
        Destroy(gameObject);//destroys the game object
    }
}
