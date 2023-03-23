using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject easyEnemyPrefab;
    Vector3 spawnPosition;
    float cameraOffset = 15;
    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI enemiesDestroyedText;
    PlayerController PlayerControllerScript;
    public int enemiesDestroyed = 0;
    float elapsedTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnEnemy", 0, 2);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateUI();
    }

    void SpawnEnemy()
    {
        

        // Get the main camera in the scene
        Camera mainCamera = Camera.main;
        GameObject easyEnemy = Instantiate(easyEnemyPrefab);

        // Calculate a random position outside of the camera's view
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float x = Random.Range(0, 2) == 0 ? -cameraWidth- cameraOffset - easyEnemy.GetComponent<Renderer>().bounds.extents.x : cameraWidth+ cameraOffset + easyEnemy.GetComponent<Renderer>().bounds.extents.x;
        float z = Random.Range(-cameraHeight- cameraOffset + easyEnemy.GetComponent<Renderer>().bounds.extents.z, cameraHeight+ cameraOffset - easyEnemy.GetComponent<Renderer>().bounds.extents.z);
        spawnPosition = new Vector3(x, 0.5f, z);
        
        easyEnemy.transform.position = spawnPosition;

    }

    void UpdateUI()
    {
        survivalTimeText.text = "Time Elapsed: " + Mathf.Round(elapsedTime);
        playerHealthText.text = "Health: " + PlayerControllerScript.playerHealth;
        enemiesDestroyedText.text = "Enemies Destroyed: " + enemiesDestroyed;
    }

    
    
}
