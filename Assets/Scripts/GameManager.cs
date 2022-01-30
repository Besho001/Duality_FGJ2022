using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerControlller playerControlller;

    // Start is called before the first frame update
    void Start()
    {
        playerControlller = FindObjectOfType<PlayerControlller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!playerControlller.isAlive)
        {
            StartCoroutine(ReloadGame());
        }
    }

    private IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
