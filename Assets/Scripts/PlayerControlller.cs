using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlller : Character
{
    private float vertical;
    private Rigidbody2D rb;
    private ParticleSystem damageParticle;

    protected override void Start()
    {
        base.Start();

        isPlayer = true;
        rb = GetComponent<Rigidbody2D>();
        damageParticle = GetComponent<ParticleSystem>();
        damageParticle.maxParticles = 0;
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        HandleMovement();
        UpdateShotDirection();
        UpdateDamageStatus();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            HandleShooting(true);
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            HandleShooting(false);
        }
        else
        {
            currentShotDelay = 0;
        }
    }

    void HandleMovement()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            vertical = Input.GetAxis("Vertical");
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position + bulletOffset).normalized;
        Vector3 acc = vertical * direction * acceleration * Time.deltaTime;

        rb.AddForce(acc, ForceMode2D.Force);

        Vector2 velocity = rb.velocity;
        velocity = Vector3.ClampMagnitude(velocity, speed);
        rb.velocity = velocity;

        //Debug.Log(direction);
    }

    void UpdateShotDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //get the vector representing the mouse's position relative to the point...
        Vector3 direction = (transform.position + bulletOffset - mousePos).normalized;

        //use atan2 to get the angle; Atan2 returns radians
        float angleRadians = Mathf.Atan2(direction.y, direction.x);

        //convert to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // angleDegrees will be in the range ]-180,180].
        if (angleDegrees < 0)
        {
            angleDegrees += 360;
        }

        //Debug.Log(angleDegrees);

        bulletRotation = angleDegrees - 90;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, bulletRotation);
    }

    void UpdateDamageStatus()
    {
        damageParticle.maxParticles = 20 * (maxHeath - health) / maxHeath;
    }

    override protected IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2();
        damageParticle.maxParticles = 0;
        GetComponent<SpriteRenderer>().color = Color.clear;
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        audioManager.audioSource.PlayOneShot(audioManager.playerDeathSound, 0.6f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(1);
        }
    }
}
