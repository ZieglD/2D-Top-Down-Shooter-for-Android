using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script zur Spielersteuerung (Bewegen und Schießen)
// Ist auf dem Player in der BasementMain Scene
public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public Joystick shootStick;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 shoot;

    public GameObject bulletPrefab;
    
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    
    void Update()
    {
        fireDelay = GameController.FireRate;
        moveSpeed = GameController.MoveSpeed;
        
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        shoot.x = shootStick.Horizontal;
        shoot.y = shootStick.Vertical;

        // legt fest, wann geschossen wird (Abstand zw. zwei projektilen).
        if ((shoot.x != 0 || shoot.y != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shoot.x, shoot.y);
            lastFire = Time.time;
        }
        
    }


    // Funktion zum Schießen von Projektilen, setzt Gravity auf 0 und legt fest, dass die Geschwindigkeit eines Projektils konstant ist.
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed
            );
    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
