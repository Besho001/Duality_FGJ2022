using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemySpawnOrder;
    public Vector3[] spawnLocationOrder;
    public int[] enemiesPerWave;
    public GameObject[] currentEnemyWave;
    public int waveNumber;
    public int spawnEnemyNumber;
    private AudioManager audioManager;
    public Text winText;


    private Vector3 cameraPosition;
    public int defeatedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        defeatedEnemies = 0;
        spawnEnemyNumber = 0;
        waveNumber = 0;
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        defeatedEnemies = 0;
        if (currentEnemyWave.Length > 0 && currentEnemyWave[0] != null)
        {
            foreach (GameObject enemy in currentEnemyWave)
            {
                if (!enemy.activeSelf)
                {
                    defeatedEnemies++;
                }
            }
        }
        
        if (currentEnemyWave.Length > 0 && defeatedEnemies == currentEnemyWave.Length)
        {
            foreach (GameObject enemy in currentEnemyWave)
            {
                Destroy(enemy);
            }

            if (waveNumber < enemiesPerWave.Length)
            {
                StartCoroutine(SpawnWave());
            }
            else
            {
                HandleWin();
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        waveNumber++;
        defeatedEnemies = 0;
        currentEnemyWave = new GameObject[enemiesPerWave[waveNumber - 1]];

        yield return new WaitForSeconds(3f);

        int j = 0;
        for (int i = spawnEnemyNumber; i < spawnEnemyNumber + enemiesPerWave[waveNumber - 1]; i++)
        {
            currentEnemyWave[j] = Instantiate(enemySpawnOrder[i], cameraPosition + spawnLocationOrder[i], Quaternion.identity);
            j++;
        }

        audioManager.audioSource.PlayOneShot(audioManager.spawnSound, 0.6f);
        spawnEnemyNumber += enemiesPerWave[waveNumber - 1];
    }

    protected void HandleWin()
    {
        if(winText.text == "")
        {
            winText.text = "Last wave cleared!";
        }
    }
}
