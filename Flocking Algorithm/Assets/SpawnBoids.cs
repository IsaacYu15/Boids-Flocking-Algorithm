using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    public GameObject boids;
    public float spawnNumber;
    public float bounds = 20;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnNumber; i ++)
        {
            GameObject boid = Instantiate(boids,
                new Vector3(Random.Range(-bounds + 1, bounds - 1), 0, Random.Range(-bounds + 1, bounds - 1)),
                Quaternion.identity);
           boid.transform.GetComponent<BoidMovement>().bounds = bounds;


        }
    }
}
