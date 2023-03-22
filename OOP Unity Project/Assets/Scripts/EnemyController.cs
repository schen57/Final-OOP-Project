using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 playerDirection;
    GameObject player;
    ParticleSystem smoke;
    AudioSource breaking;
    bool hasPlayedSound = false;
    private Rigidbody enemyRb;
    float speed=1;
    int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        smoke = transform.Find("Smoke").GetComponent<ParticleSystem>();
        breaking = transform.Find("SFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        PlayDestructionSequence();
    }

    void EnemyMovement()
    {
        playerDirection = new Vector3(player.transform.position.x, 0.8f, player.transform.position.z) - transform.position;
        enemyRb.velocity = playerDirection * speed;
    }

    void PlayDestructionSequence()
    {
        if (health <= 0 && !hasPlayedSound)
        {
            hasPlayedSound = true;
            if (smoke != null)
            {
                smoke.transform.parent = null; // Detach the smoke particle system from the enemy object
                breaking.transform.parent = null;
                breaking.PlayOneShot(breaking.clip);
                smoke.Play(); // Play the smoke particle system
            }
            Destroy(gameObject);
        }
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
