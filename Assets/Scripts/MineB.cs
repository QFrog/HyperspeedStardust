using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineB : MonoBehaviour
{
    public float xChange = .1f;//1.0f;
    public float yChange = .1f;//1.0f;
    public float zChange = .1f;//1.0f;
    public float increase = .01f;
    private Animator anim;
    private Rigidbody2D rb2d;//rigidbody2D
    public GameObject explosionPrefab;
    //public float initial = .01f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        //change its initial size to be small
        transform.localScale = new Vector3(xChange, yChange, zChange);//update size
    }

    // Update is called once per frame
    void Update()
    {
        //approach screen
        //if size is less than X, increase
        if (xChange < 1.25f)
        {
            transform.localScale = new Vector3(xChange, yChange, zChange);//update size
            xChange += increase;//update width
            yChange += increase;//update height
        }
        else
        {
            StartCoroutine("Timer");//start the timer
        }
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
        else if((coll.gameObject.tag == "Laser"))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 3.5f);
            Destroy(gameObject);
        }
    }

    //destroy the mine after so much time
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(2.0f);//wait a little before following the player
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 3.5f);
        Destroy(gameObject);
    }
}
