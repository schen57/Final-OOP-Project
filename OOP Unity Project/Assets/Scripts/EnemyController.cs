using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Initial class to be initiated with different enemy properties
public class EnemyProperties
{
    public int health;
    public float movementSpeed;
    public int dmg;
    public float mass;
    public int def;
    public EnemyProperties(int health, float movementSpeed, int dmg, float mass,int def)
    {
        this.health = health;
        this.movementSpeed = movementSpeed;
        this.dmg = dmg;
        this.mass = mass;
        this.def = def;
    }
}

public class EnemyController : MonoBehaviour
{
    Vector3 playerDirection;
    GameObject player;
    ParticleSystem smoke;
    AudioSource breaking;
    bool hasPlayedSound = false;
    private Rigidbody enemyRb;
    public TextMeshProUGUI dmgText;
    PlayerController playerControllerScript;
    
    
    GameManager gameManagerScript;

    // Set a public enemy type & setting the various enemy properties
    public enum EnemyType { Easy, Tank, Uneven };
    public EnemyType enemyType;
    public EnemyProperties enemyProperties;

    // Avoidance behavior variables
    public float avoidanceRadius = 2f;
    public float avoidanceForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        smoke = transform.Find("Smoke").GetComponent<ParticleSystem>();
        breaking = transform.Find("SFX").GetComponent<AudioSource>();
        //EnemyProperties enemyProperties;

        // check what type of enemy is intiated
        switch (enemyType)
        {
            case EnemyType.Easy:
                enemyProperties = new EnemyProperties(100, 3f, 10, 1f,1);
                break;
            case EnemyType.Tank:
                enemyProperties = new EnemyProperties(200, 1f, 20, 4f,10);
                break;
            case EnemyType.Uneven:
                enemyProperties = new EnemyProperties(50, 5f, 5, 0.5f,2);
                break;
            default:
                Debug.LogError("Invalid enemy type!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        PlayDestructionSequence();
        // Create a cap for the enemy Speed
        if (enemyProperties.movementSpeed <= 10)
        {
            enemyProperties.movementSpeed += Time.deltaTime;
        }
        
    }

    void EnemyMovement()
    {
        playerDirection = new Vector3(player.transform.position.x, 0.8f, player.transform.position.z) - transform.position;

        // Calculate avoidance force to prevent overlapping
        Collider[] colliders = Physics.OverlapSphere(transform.position, avoidanceRadius);
        Vector3 avoidanceDirection = Vector3.zero;
        int avoidanceCount = 0;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") && collider.gameObject != gameObject)
            {
                Vector3 direction = transform.position - collider.gameObject.transform.position;
                avoidanceDirection += direction.normalized / direction.magnitude;
                avoidanceCount++;
            }
        }

        if (avoidanceCount > 0)
        {
            avoidanceDirection /= avoidanceCount;
            playerDirection += avoidanceDirection.normalized * avoidanceForce;
        }

        enemyRb.velocity = playerDirection.normalized * enemyProperties.movementSpeed;
    }

    void PlayDestructionSequence()
    {
        if (enemyProperties.health <= 0 && !hasPlayedSound)
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
            gameManagerScript.enemiesDestroyed += 1;
        }
    }
    private IEnumerator HideDamageText(TextMeshProUGUI dmgText)
    {
        yield return new WaitForSeconds(.5f);
        dmgText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magic") && !PlayerController.isGameOver)
        {
            // Code to damage the enemy or perform other actions
            enemyProperties.health -= (20 - enemyProperties.def);
            dmgText.gameObject.SetActive(true);
            // Create a new instance of the damage text and position it at the collision point
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(closestPoint);
            dmgText.transform.position = screenPoint;


            // Set the damage text value
            dmgText.text = (20 - enemyProperties.def).ToString();
            // Destroy the damage text object after a delay
            StartCoroutine(HideDamageText(dmgText));
            Material originalMaterial = GetComponent<Renderer>().material;
            Material dmgMaterial = playerControllerScript.dmgMaterial;
            StartCoroutine(SwapMaterialCoroutine(originalMaterial, dmgMaterial));

        }

        if (other.gameObject.CompareTag("Player")&&!PlayerController.isGameOver)
        {
            Destroy(gameObject);
        }
    }

    // Draw Gizmos for avoidance behavior
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }

    IEnumerator SwapMaterialCoroutine(Material originalMaterial, Material dmgMaterial)
    {
        GetComponent<Renderer>().material = dmgMaterial;
        yield return new WaitForSeconds(.01f);
        GetComponent<Renderer>().material = originalMaterial;
    }
}
