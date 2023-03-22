using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody rb;
    public GameObject fireBall;

    public int numObjects = 8; // the number of objects to spawn
    public float radius = 5.0f; // the radius of the ring
    public float offsetY = 0.0f; // the height offset of the ring
    private float xBounds = 34;
    private float zBounds = 16;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Repeatedly spawn fireballs
        InvokeRepeating("SpawnFireBalls", 0, 1f);
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MovementControl();

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
        for (int i = 0; i < numObjects; i++)
        {
            float angle = i * Mathf.PI * 2f / numObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), offsetY, Mathf.Sin(angle)) * radius;
            GameObject obj = Instantiate(fireBall, transform.position + pos, Quaternion.identity);
            obj.transform.LookAt(transform.position);
            obj.transform.Rotate(0, 360f / numObjects * i, 0);
            obj.transform.Rotate(0, 90/ numObjects * i, 90);

        }
    }
}

/*
3/21/2023 Tuesday Morning
Recap of the next steps:
Observed Problems:
1. The ring of fireballs are moving in the same direction regardless of where the fireball is pointing
2. Each of the fireball is rotated 90 degrees but they aren't pointing in the right direction
3. This probably has something to do with rotating only 90 degrees but where each fireball is pointed isn't correct. This needs to be fixed

Proposed next step:
The rotation along z axis need to be changed for each fireball. This seems to be fixable by adjusting the relative angle of each fireball

 */