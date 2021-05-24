using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineA : MonoBehaviour
{
    public float speed = 1.0f;//speed of the enemy
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject player;//reference to the player
    public float waitTime = 2f;
    public int health = 1;
    public GameObject enemyBullet;
    private Animator anim;
    public GameObject explosionPrefab;
    private AudioSource deathSound;
    private AudioClip deathLength;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(enemyBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((transform.position.x - (speed / 100)), transform.position.y, 0.0f);//move forward at a constant rate

        //if the player gets too close trigger the detonation process
        if (Vector3.Distance(transform.position, player.transform.position) < 4.0f)
        {
            //currently activates for enemy bullets
            //StartCoroutine("Timer");//start the timer
        }
        if (transform.position.x == -5.5f)
        {
            Destroy(gameObject);//destroy the game object
        }
    }

    //when the player gets too close, it will detonate a short bit later
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(waitTime);//wait a little before following the player
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 3.5f);
        Destroy(gameObject);
    }

    //detonate immediatly when the player touches it
    void OnTriggerEnter2D(Collider2D coll)
    {
        //ignore collision if not player
        if (coll.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(coll.GetComponent<Collider2D>(), coll);
        }
        //if player gets hit by mine
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("mine died from touching player");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5
            Destroy(gameObject); //destories the original gameobject
        }
        //if health is below the threshold die
        if (coll.gameObject.tag == "Laser" || coll.gameObject.tag == "Missile")
        {
            Debug.Log("mine died from player");
            Destroy(coll.gameObject);
            //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);//destroy after the animation is finished
            //Destroy(gameObject);

            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation); // creates the explosion and plays sound on awake
            Destroy(explosion, 3.5f); // destories it after 3.5
            Destroy(gameObject); //destories the original gameobject
        }
    }
}


