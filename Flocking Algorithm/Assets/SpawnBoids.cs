using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    public List<GameObject> BoidsList;
    public GameObject Boids;
    public float SpawnNumber;
    public float Bounds = 20;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SpawnNumber; i ++)
        {
            GameObject Boid = Instantiate(Boids,
                new Vector3(Random.Range(-Bounds + 1, Bounds - 1), 0, Random.Range(-Bounds + 1, Bounds - 1)),
                Quaternion.identity);
            Boid.transform.GetComponent<BoidMovement>().Bounds = Bounds;
            Boid.transform.GetComponent<BoidMovement>().BoidManager = this;
            BoidsList.Add(Boid);
        }
    }
}
