using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody rb;
    public GameObject fireBall;
    private AudioSource damageSound;
    private AudioSource powerUpSfx;
    public static bool isGameOver;

    public int numObjects = 1; // the number of objects to spawn
    public float radius = 1.0f; // the radius of the ring
    public float offsetY = 0.0f; // the height offset of the ring
    private float xBounds = 34;
    private float zBounds = 16;
    public int playerHealth = 100;
    public EnemyController enemyControllerScript;
    public AudioSource[] sfx = new AudioSource[2];
    public Material dmgMaterial;



    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        rb = GetComponent<Rigidbody>();
        sfx = GetComponents<AudioSource>();
        damageSound = sfx[0];
        powerUpSfx = sfx[1];


        // Repeatedly spawn fireballs
        if (!isGameOver)
        {
            InvokeRepeating("SpawnFireBalls", 0, 1f);
        }
        
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isGameOver)
        {
            MovementControl();
        }else if (playerHealth <= 0)
        {
            isGameOver = true;
        }
    }

    void MovementControl()
    {   // Constrain movement within a movement 
        if (transform.position.x < -xBounds)
        {
            transform.position= new Vector3(-xBounds,transform.position.y,transform.position.z);
        }else if (transform.position.x > xBounds)
        {
            transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
        }else if(transform.position.z > zBounds)
        {
            transform.position = new Vector3(transform.position.x,  transform.position.y, zBounds);
        }else if (transform.position.z < -zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBounds);
        }

        //Movement controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = Vector3.zero;
        }
        

        
    }

    void SpawnFireBalls()
    {
        if (isGameOver != true)
        {
            for (int i = 0; i < numObjects; i++)
            {
                float angle = 360 / numObjects;
                Vector3 pos = new Vector3(Mathf.Cos(angle), offsetY, Mathf.Sin(angle)) * radius;
                GameObject obj = Instantiate(fireBall, transform.position + pos, Quaternion.identity);
                obj.transform.LookAt(transform.position);
                obj.transform.Rotate(0, 360f / numObjects * i, 0); // by itself creates the circular pattern
                                                                   //obj.transform.Rotate(0, 90/ numObjects * i, 90); // by itself creates the fan like pattern

            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && playerHealth>0)
        {
            enemyControllerScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
            damageSound.PlayOneShot(damageSound.clip);
            playerHealth -= enemyControllerScript.enemyProperties.dmg;
            Material originalMaterial = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = dmgMaterial;
            StartCoroutine(SwapMaterialBack(originalMaterial, 0.01f));
        }
        else if (playerHealth <= 0)
        {
            isGameOver = true;
        }else if (other.gameObject.CompareTag("PowerUp") && playerHealth > 0)
        {
            numObjects += 1;
            powerUpSfx.PlayOneShot(powerUpSfx.clip);
        }
    }

    IEnumerator SwapMaterialBack(Material originalMaterial, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Swap back to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }
}
