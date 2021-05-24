using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomberShots : MonoBehaviour
{
    public float xChange = 1.0f;
    public float yChange = 1.0f;
    public float zChange = 1.0f;
    public float increase = .01f;
    public Vector2 destination;
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    public float speed = .05f;//speed of the enemy
    public float xCoord = 0;
    public float yCoord = 0;
    public GameObject bomber;
    public int shot;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        bomber = GameObject.Find("bomberA(Clone)");
        xCoord = player.transform.position.x;
        yCoord = player.transform.position.y;
        destination = new Vector2(player.transform.position.x, player.transform.position.y);

        BomberA bomberScript = bomber.GetComponent<BomberA>();
        //Debug.Log(bomberScript.shotNum);
        shot = bomberScript.shotNum;
        //Debug.Log(shot);
        if (shot == 1)//slightly above
        {
            destination = new Vector2(player.transform.position.x-2.0f, player.transform.position.y+0.0f);
        }
        else if (shot == 2)//directly towards player
        {
            destination = new Vector2(player.transform.position.x-2.0f, player.transform.position.y-1.5f);
        }
        else if (shot == 3)//slightly below
        {
            destination = new Vector2(player.transform.position.x-2.0f, player.transform.position.y-3.0f);
        }
        else if (shot == 4)//slightly below
        {
            destination = new Vector2(player.transform.position.x-2.0f, player.transform.position.y-4.5f);
        }
        else if (shot == 5)//slightly below
        {
            destination = new Vector2(player.transform.position.x-2.0f, player.transform.position.y-6.0f);
        }
        //destination = new Vector2(player.transform.position.x, player.transform.position.y);
        //update destination as players position
        //destination = player.transform.position;// new Vector2(xCoord, yCoord);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move towards the players previous location
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    }

    //detonate immediatly when the player touches it
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {
            Destroy(gameObject);//destroy the game object if player touches it
        }
    }
}
