using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bouncesLeft;
    public bool playerBullet;
    public bool isDamageBullet;
    public float speed;
    public int damage;
    private Vector2 moveVector;
    public bool exitedShooter;

    private const float maxDistFromPlayer = 2.7f;
    private GameObject player;
    private Rigidbody2D rb;

    void Start()
    {
        bouncesLeft = 1;
        exitedShooter = false;
        moveVector = new Vector2(0, -speed);
        rb = GetComponent<Rigidbody2D>();

        if (isDamageBullet)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (!isDamageBullet)
        {
            damage = -damage;
        }

        if (FindObjectOfType<PlayerControlller>())
        {
            player = GameObject.FindObjectOfType<PlayerControlller>().gameObject;
        }
    }

    void Update()
    {
        gameObject.transform.Translate(moveVector * Time.deltaTime, Space.Self);

        if (GetComponent<Renderer>() && !GetComponent<Renderer>().isVisible)
        {
            //Debug.Log("Not Visible");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exitedShooter &&
            collision.GetComponent<Character>())
        {
            if (collision.gameObject.GetComponent<Character>().normalDamage)
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(-damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If object didn't hit 
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitedShooter = true;
    }
}
