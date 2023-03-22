using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject easyEnemyPrefab;
    Vector3 spawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        

        // Get the main camera in the scene
        Camera mainCamera = Camera.main;
        GameObject easyEnemy = Instantiate(easyEnemyPrefab);

        // Calculate a random position outside of the camera's view
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float x = Random.Range(0, 2) == 0 ? -cameraWidth - easyEnemy.GetComponent<Renderer>().bounds.extents.x : cameraWidth + easyEnemy.GetComponent<Renderer>().bounds.extents.x;
        float z = Random.Range(-cameraHeight + easyEnemy.GetComponent<Renderer>().bounds.extents.z, cameraHeight - easyEnemy.GetComponent<Renderer>().bounds.extents.z);
        spawnPosition = new Vector3(x, 0.5f, z);
        
        easyEnemy.transform.position = spawnPosition;

    }
}
