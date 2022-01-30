using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip shootSound;
    public AudioClip healSound;
    public AudioClip playerDeathSound;
    public AudioClip enemyDeathSound;
    public AudioClip spawnSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hitSound = Resources.Load<AudioClip>("Hit");
        shootSound = Resources.Load<AudioClip>("Shoot");
        healSound = Resources.Load<AudioClip>("Heal");
        playerDeathSound = Resources.Load<AudioClip>("PlayerDeath");
        enemyDeathSound = Resources.Load<AudioClip>("EnemyDeath");
        spawnSound = Resources.Load<AudioClip>("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
