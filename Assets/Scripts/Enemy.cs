using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected Rigidbody2D rb;
    [SerializeField]
    protected bool shootDamageBullets;
    [SerializeField]
    protected int bulletAlterToRate;
    [SerializeField]
    protected int bulletAlterBackRate;
    protected int currentBulletAlterRate;
    protected bool altered;

    protected override void Start()
	{
        base.Start();

        isPlayer = false;
        rb = GetComponent<Rigidbody2D>();
        currentBulletAlterRate = bulletAlterToRate;
    }

    protected override void Update()
    {
        base.Update();
    }

    override protected IEnumerator HandleDeath()
    {
        yield return null;
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        audioManager.audioSource.PlayOneShot(audioManager.enemyDeathSound, 0.6f);
        gameObject.SetActive(false);  
    }

    override protected void HandleShooting(bool _isDamageBullet)
    {
        base.HandleShooting(_isDamageBullet);

        if (bulletAlterToRate > 0 && bulletAlterBackRate > 0 && currentShotDelay <= 0)
        {
            if (!altered)
            {
                if (currentBulletAlterRate > 0)
                {
                    currentBulletAlterRate--;
                }

                if (currentBulletAlterRate == 0)
                {
                    shootDamageBullets = !shootDamageBullets;
                    _isDamageBullet = !shootDamageBullets;
                    currentBulletAlterRate = bulletAlterBackRate;
                    altered = !altered;
                }
            }
            else
            {
                if (currentBulletAlterRate > 0)
                {
                    currentBulletAlterRate--;
                }

                if (currentBulletAlterRate == 0)
                {
                    shootDamageBullets = !shootDamageBullets;
                    _isDamageBullet = !shootDamageBullets;
                    currentBulletAlterRate = bulletAlterToRate;
                    altered = !altered;
                }
            }
        }
    }
}
