using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public float speed = 10.0f; // the speed at which the fireball moves
    public Vector3 direction = Vector3.forward; // the direction in which the fireball moves
    private float xBounds = 34;
    private float zBounds = 16;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x < -xBounds | transform.position.x > xBounds)
        {
            Destroy(gameObject);
        }else if(transform.position.z < -zBounds | transform.position.z > zBounds)
        {
            Destroy(gameObject);
        }
    }
}
