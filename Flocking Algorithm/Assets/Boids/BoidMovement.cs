using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    public bool subjectOfStudy;

    public LayerMask boundaries;
    public LayerMask boids;

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
        transform.rotation = Quaternion.LookRotation(velocity);
        transform.position = transform.position + transform.forward * speed;

        if (Mathf.Abs(transform.position.x) > bounds || Mathf.Abs(transform.position.z) > bounds )
        {
            respawnBoid();
        }

        avoidCollisions();
    }

    void respawnBoid ()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -velocity, out hit, Mathf.Infinity, boundaries))
        {
            transform.position = hit.transform.position;
        }

    }

    //NEEDS WORK
    void avoidCollisions ()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5);
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.transform.gameObject.layer == Mathf.RoundToInt(Mathf.Log(boids.value,2))
                && hitCollider.transform.gameObject != this.gameObject)
            {
                float angle = Vector3.Angle(transform.forward, hitCollider.gameObject.transform.position - transform.position);
                if (Mathf.Abs(angle) < 120)
                {
                    //Debug.DrawLine(hitCollider.transform.position, transform.position, Color.black);
                    velocity = (velocity - hitCollider.gameObject.GetComponent<BoidMovement>().velocity).normalized;
                }

            }
        }
    }
}
