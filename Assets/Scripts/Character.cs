using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected bool isPlayer;
    protected int maxHeath;
    [SerializeField]
    protected int health;
    [HideInInspector]
    public bool isAlive;
    [SerializeField]
    protected float acceleration;
    [SerializeField]
    protected float speed;
    protected float damageBlinkTIme = 0.03f;

    [SerializeField]
    protected float shotDelay;
    protected float currentShotDelay;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected int bulletDamage;
    [SerializeField]
    protected float bulletRotation;
    [SerializeField]
    protected Vector3 bulletOffset;
    protected Quaternion rotationQ;
    [SerializeField]
    protected GameObject bulletPrefab;
    protected GameObject bullet;

    public GameObject deathParticle;

    public bool normalDamage;
    protected Color characterColor;

    protected AudioManager audioManager;

    protected virtual void Start()
	{
        maxHeath = health;
        currentShotDelay = shotDelay;
        isAlive = true;
        audioManager = FindObjectOfType<AudioManager>();

        if (normalDamage)
        {
            characterColor = Color.yellow;
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            characterColor = Color.magenta;
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    protected virtual void Update()
    {
        if (health <= 0 && isAlive)
		{
            StartCoroutine(HandleDeath());
            isAlive = false;
		}
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive)
		{
            return;
		}

        if (health - damage <= maxHeath)
        {
            health -= damage;
        }

        if (health != 0 && damage > 0)
		{
            StartCoroutine(Blink(Color.red));
            audioManager.audioSource.PlayOneShot(audioManager.hitSound, 0.6f);
        }
        else if (health != 0 && damage < 0)
        {
            StartCoroutine(Blink(Color.green));
            audioManager.audioSource.PlayOneShot(audioManager.healSound, 0.6f);
        }
    }

    protected virtual void HandleShooting(bool _isDamageBullet)
    {
        if (currentShotDelay <= 0)
        {
            audioManager.audioSource.PlayOneShot(audioManager.shootSound, 0.25f);

            rotationQ = Quaternion.Euler(0, 0, bulletRotation);
            bullet = Instantiate(bulletPrefab, transform.position + bulletOffset, rotationQ);
            bullet.GetComponent<Bullet>().playerBullet = isPlayer;
            bullet.GetComponent<Bullet>().speed = bulletSpeed;
            bullet.GetComponent<Bullet>().damage = bulletDamage;
            bullet.GetComponent<Bullet>().isDamageBullet = _isDamageBullet;

            currentShotDelay = shotDelay;
        }
        else if (currentShotDelay > 0)
        {
            currentShotDelay -= Time.deltaTime;
        }
    }

    protected virtual IEnumerator Blink(Color color)
	{
        gameObject.GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(damageBlinkTIme);
        gameObject.GetComponent<SpriteRenderer>().color = characterColor;
    }

    protected virtual IEnumerator HandleDeath() { yield return null; }
}
