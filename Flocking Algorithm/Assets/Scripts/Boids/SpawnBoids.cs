using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
    public GameObject ParentZones;
    public List<GameObject> NoSpawnZones;
    public List<GameObject> BoidsList;
    public GameObject Boids;
    public float SpawnNumber;
    public float Bounds = 20;

    public float AvoidingWeight;
    public float AligningWeight;
    public float CohesionWeight;
    public float BoundsWeight;

    public float AvoidingDistance;
    public float AligningDistance;
    public float CohesionDistance;

    public float MinSpeed;
    public float MaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        BoidsList    = new List<GameObject>();
        NoSpawnZones = new List<GameObject>();

        //prob more efficient to assign in editor but this is just easier :D
        foreach (Transform Child in ParentZones.transform)
        {
            NoSpawnZones.Add(Child.gameObject);
        }

        for (int i = 0; i < SpawnNumber; i ++)
        {
            GameObject Boid = Instantiate(Boids, GenerateRandomPosition(), Quaternion.identity);
            BoidMovement BoidMovement = Boid.transform.GetComponent<BoidMovement>();
            UpdateBoids(BoidMovement);

            BoidsList.Add(Boid);
        }
    }

    public void UpdateBoids(BoidMovement BoidMovement)
    {
        //allows for us to reuse the function
        if (!BoidMovement.BoidManager)
        {
            BoidMovement.BoidManager = this;
            BoidMovement.Speed = Random.Range(MinSpeed, MaxSpeed);
        }

        BoidMovement.Bounds = Bounds;

        BoidMovement.AligningWeight = this.AligningWeight;
        BoidMovement.AvoidingWeight = this.AvoidingWeight;
        BoidMovement.CohesionWeight = this.CohesionWeight;
        BoidMovement.BoundsWeight   = this.BoundsWeight;

        BoidMovement.AligningDistance = this.AligningDistance;
        BoidMovement.AvoidingDistance = this.AvoidingDistance;
        BoidMovement.CohesionDistance = this.CohesionDistance;
    }

    public Vector3 GenerateRandomPosition()
    {
        bool ValidSpawn = true;
        Vector3 RandomPosition;

        do
        {
            RandomPosition = new Vector3(Random.Range(-Bounds + 1, Bounds - 1),
                                         Random.Range(-Bounds + 1, Bounds - 1),
                                         Random.Range(-Bounds + 1, Bounds - 1)
                                        );

            foreach (GameObject Go in NoSpawnZones)
            {
                ValidSpawn = Go.GetComponent<BoxCollider>().bounds.Contains(RandomPosition);

                if (!ValidSpawn)
                {
                    break;
                }
            }

        } while (!ValidSpawn);


        return RandomPosition + transform.position;
    }
}
