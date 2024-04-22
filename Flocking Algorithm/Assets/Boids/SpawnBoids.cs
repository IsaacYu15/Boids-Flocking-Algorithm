using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoids : MonoBehaviour
{
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
        for (int i = 0; i < SpawnNumber; i ++)
        {
            GameObject Boid = Instantiate(Boids,
                new Vector3(Random.Range(-Bounds + 1, Bounds - 1), Random.Range(-Bounds + 1, Bounds - 1), Random.Range(-Bounds + 1, Bounds - 1)),
                Quaternion.identity);

            BoidMovement BoidMovement = Boid.transform.GetComponent<BoidMovement>();

            BoidMovement.Bounds         = Bounds;
            BoidMovement.BoidManager    = this;
            BoidMovement.AligningWeight = this.AligningWeight;
            BoidMovement.AvoidingWeight = this.AvoidingWeight;
            BoidMovement.CohesionWeight = this.CohesionWeight;
            BoidMovement.BoundsWeight   = this.BoundsWeight;

            BoidMovement.AligningDistance = this.AligningDistance;
            BoidMovement.AvoidingDistance = this.AvoidingDistance;
            BoidMovement.CohesionDistance = this.CohesionDistance;

            BoidMovement.Speed          = Random.Range(MinSpeed, MaxSpeed);

            BoidsList.Add(Boid);
        }
    }

    private void Update()
    {
        UpdateBoids();
    }

    public void UpdateBoids()
    {
        foreach (GameObject Boid in BoidsList)
        {
            BoidMovement BoidMovement = Boid.transform.GetComponent<BoidMovement>();

            BoidMovement.AligningWeight = this.AligningWeight;
            BoidMovement.AvoidingWeight = this.AvoidingWeight;
            BoidMovement.CohesionWeight = this.CohesionWeight;
            BoidMovement.BoundsWeight   = this.BoundsWeight;

            BoidMovement.AligningDistance = this.AligningDistance;
            BoidMovement.AvoidingDistance = this.AvoidingDistance;
            BoidMovement.CohesionDistance = this.CohesionDistance;
        }

    }
}