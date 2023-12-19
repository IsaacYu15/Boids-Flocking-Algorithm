using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    public LayerMask boundaries;

    public float bounds;
    public float speed = 0.5f;
    Vector3 velocity;

    public Color[] colors;

    void Start()
    {
        //set random velocity
        velocity = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        velocity = velocity.normalized;

        //set random colour 
        SpriteRenderer renderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        renderer.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + velocity * speed;
        transform.rotation = Quaternion.LookRotation(velocity);

        if (Mathf.Abs(transform.position.x) > bounds || Mathf.Abs(transform.position.z) > bounds )
        {
            respawnBoid();
        }
        
    }

    void respawnBoid ()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -velocity, out hit, Mathf.Infinity, boundaries))
        {
            transform.position = hit.transform.position;
        }

    }
}
