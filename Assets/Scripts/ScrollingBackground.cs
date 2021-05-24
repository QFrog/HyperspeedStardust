using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    Material material;
    public Material material2;
    public Material material3;
    Vector2 offset;

    public float xVelocity, yVelocity;
    public float speed;

    private void Awake()
    {
        if (GameManager.levelTrack % 2 == 0)
            GetComponent<Renderer>().material = material2;
        if (GameManager.levelTrack % 3 == 0)
            GetComponent<Renderer>().material = material3;
        material = GetComponent<Renderer>().material;
    }
    void Start()
    {
        offset = new Vector2(xVelocity, yVelocity);
    }
    void Update()
    {
        speed = UIController.speed;
        material.mainTextureOffset += offset * Time.deltaTime * speed;
    }
}
