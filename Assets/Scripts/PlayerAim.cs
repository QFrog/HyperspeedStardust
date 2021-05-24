using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject laser;
    public GameObject missile;

    private Transform ship;
    private Vector2 lookDirection;
    private Vector3 MouseCoords;
    private float lookAngle;

    private void Start()
    {
        ship = GetComponent<Transform>();
    }
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);// - 90f);
        if (Input.GetButtonDown("Fire"))
            FireLaser();
        if (Input.GetButtonDown("Fire2") && UIController.ammo > 0)
            FireMissile();
        MoveCrossHair();
    }
    private void FireLaser()
    {
        GameObject firedLaser = Instantiate(laser, ship.position, ship.rotation);
        firedLaser.GetComponent<Rigidbody2D>().velocity = ship.right * 10f;
        Destroy(firedLaser, 4.0f);
    }
    private void FireMissile()
    {
        //transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
        GameObject firedMissile = Instantiate(missile, ship.position, ship.rotation);
        firedMissile.GetComponent<Rigidbody2D>().velocity = ship.right * 20f;
        Destroy(firedMissile, 4.0f);
        UIController.ammo--;
    }
    private void MoveCrossHair()
    {
        MouseCoords = Input.mousePosition;
        Camera.main.ScreenToWorldPoint(MouseCoords);
        crosshair.transform.position = Vector2.Lerp(transform.position, MouseCoords, 1f);
    }
}
