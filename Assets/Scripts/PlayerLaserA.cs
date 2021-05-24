using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserA : MonoBehaviour
{
    private Rigidbody2D rb2d;//rigidbody2D
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    //if collision with something other than player laser and player then destroy
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag != "Player") && (coll.gameObject.tag != "Laser") && (coll.gameObject.tag != "EnemyShot"))
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }
    */
}
