using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab = new GameObject[3];
    public GameObject powerUpPrefab;
    Vector3 spawnPosition;
    float cameraOffset = 15;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemiesDestroyedText;
    public TextMeshProUGUI projectilesText;
    PlayerController PlayerControllerScript;
    public int enemiesDestroyed = 0;
    float elapsedTime = 0;
    public GameObject gameOverOverlay;
    public AudioSource mainBackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {

        if (gameOverOverlay != null)
        {
            gameOverOverlay.SetActive(false);
        }
        mainBackgroundMusic = GetComponent<AudioSource>();
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnEnemy", 0, 5);
        InvokeRepeating("SpawnPowerUp", 0, 15);
        UpdateUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isGameOver != true)
        {
            elapsedTime += Time.deltaTime;
            UpdateUI();
        }
        else
        {
            
            CancelInvoke("SpawnEnemy");
            CancelInvoke("SpawnPowerUp");
            GameOver();
        }

        if (UIController.isGamePaused)
        {
            PlayerControllerScript.sfx[0].volume = 0.1f;
        }
        else if (!UIController.isGamePaused)
        {
            PlayerControllerScript.sfx[0].volume = 1;
        }
        

    }

    void SpawnEnemy()
    {
        int swarmSize;
        swarmSize = Mathf.RoundToInt(elapsedTime / 20);

        // Get the main camera in the scene
        Camera mainCamera = Camera.main;
        // Calculate a random position outside of the camera's view
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Spawn a swarm of enemy that's tied to how long the time has elapsed
        for (int i = 0; i<= swarmSize; i++)
        {
            // Adjust the spawn probability
            int probIndex = Random.Range(0, 100);
            int enemyIndex=0;
            if (probIndex < 60)
            {
                enemyIndex = 0;
            }else if(probIndex < 90)
            {
                enemyIndex = 1;
            }else if (probIndex < 100)
            {
                enemyIndex = 2;
            }
            GameObject enemy = Instantiate(enemyPrefab[enemyIndex]);
            float x = Random.Range(0, 2) == 0 ? -cameraWidth - cameraOffset - enemy.GetComponent<Renderer>().bounds.extents.x : cameraWidth + cameraOffset + enemy.GetComponent<Renderer>().bounds.extents.x;
            float z = Random.Range(-cameraHeight - cameraOffset + enemy.GetComponent<Renderer>().bounds.extents.z, cameraHeight + cameraOffset - enemy.GetComponent<Renderer>().bounds.extents.z);
            spawnPosition = new Vector3(x, 0.5f, z);

            enemy.transform.position = spawnPosition;
        }
        

    }

    void SpawnPowerUp()
    {
        // Get the main camera in the scene
        Camera mainCamera = Camera.main;
        // Calculate a random position outside of the camera's view
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float xPos= Random.Range(-cameraWidth/2, cameraWidth/2);
        float zPos = Random.Range(-cameraHeight / 2, cameraHeight / 2);
        Vector3 powerUpSpawnPos = new Vector3(xPos, 0.649f, zPos);
        GameObject powerUp;
        powerUp = GameObject.Find("PowerUp");
        if (powerUp == null)
        {
            powerUp = Instantiate(powerUpPrefab, powerUpSpawnPos, Quaternion.identity);
        }
        else
        {
            powerUp.SetActive(true);
            powerUp.transform.position = powerUpSpawnPos;
        }

        
    }

    void UpdateUI()
    {
        survivalTimeText.text = "Time Elapsed: " + Mathf.Round(elapsedTime);
        playerHealthText.text = "Health: " + PlayerControllerScript.playerHealth;
        enemiesDestroyedText.text = "Enemies Destroyed: " + enemiesDestroyed;
        projectilesText.text = "#Projectiles: " + PlayerControllerScript.numObjects;
    }

    void GameOver()
    { 
        gameOverOverlay.SetActive(true);
        mainBackgroundMusic.Stop();
    }

    
    
}
