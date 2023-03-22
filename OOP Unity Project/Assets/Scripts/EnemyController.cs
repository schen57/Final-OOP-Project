using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 playerDirection;
    GameObject player;
    private Rigidbody enemyRb;
    float speed=1;
    int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyMovement()
    {
        playerDirection = new Vector3(player.transform.position.x, 0.8f, player.transform.position.z) - transform.position;
        enemyRb.velocity = playerDirection * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magic"))
        {
            // Code to damage the player or perform other actions
            health -= 20;
            Debug.Log(health);
        }
    }
}
