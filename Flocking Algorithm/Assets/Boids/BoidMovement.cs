using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    public SpawnBoids BoidManager;
    public LayerMask BoundsLayer;

    public float Bounds;
    public float Speed = 0.5f;
    Vector3 Heading;

    public float MaxFOV;

    public Color[] colors;

    public float AvoidingWeight;
    public float AligningWeight;
    public float CohesionWeight;
    public float BoundsWeight;

    public float AvoidingDistance;
    public float AligningDistance;
    public float CohesionDistance;

    void Start()
    {
        //set random velocity
        Heading = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Heading = Heading.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Heading = CalculateNewHeading();

        Quaternion NewRotation = Quaternion.LookRotation(Heading);
        transform.rotation = Quaternion.Lerp(transform.rotation, NewRotation, Time.deltaTime * 5);

        transform.position += transform.forward * Speed * Time.deltaTime;

    }

    List<GameObject> GetNeighbours(float DistanceWeight)
    {
        //allocation of memory and deallocation is quite inefficient
        List<GameObject> Neighbours = new List<GameObject>();

        foreach (GameObject Boid in BoidManager.BoidsList)
        {
            if (BoidInFOV(Boid.transform.position, MaxFOV) && 
                BoidInRange(Boid.transform.position, DistanceWeight) && 
                Boid != this.gameObject)
            {
                Neighbours.Add(Boid);
            }
        }

        return Neighbours;
    }

    bool BoidInFOV(Vector3 OtherBoid, float Threshold)
    {
        return Vector3.Angle(transform.forward, OtherBoid - transform.position) <= Threshold;
    }

    bool BoidInRange(Vector3 OtherBoid, float Threshold)
    {
        return Vector3.Distance(transform.position, OtherBoid) <= Threshold;
    }

    Vector3 CalculateNewHeading()
    {
        Vector3 NewHeading = Vector3.zero;
        Vector3 Avoiding =  AvoidingVector(GetNeighbours(AvoidingDistance))  * AvoidingWeight;
        Vector3 Aligning =  AlignmentVector(GetNeighbours(AligningDistance)) * AligningWeight;
        Vector3 Cohesive = CohesionVector(GetNeighbours(CohesionDistance)) * CohesionWeight;
        Vector3 Bounds = CalculateBoundsVector() * BoundsWeight;

        NewHeading = NewHeading + Avoiding + Aligning + Cohesive + Bounds;

        if (NewHeading == Vector3.zero)
        {
            return transform.forward;
        }

        return NewHeading.normalized;
    }

    Vector3 AvoidingVector (List<GameObject> Neighbours)
    {
        if (Neighbours.Count == 0 )
        {
            return Vector3.zero;
        }

        Vector3 AvoidanceVector = transform.forward;

        foreach(GameObject Boid in Neighbours)
        {
            AvoidanceVector += (transform.position - Boid.transform.position);
        }
        AvoidanceVector /= Neighbours.Count;
        return AvoidanceVector.normalized;
    }

    Vector3 AlignmentVector(List<GameObject> Neighbours)
    {
        if (Neighbours.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 AligningVector = transform.forward;

        foreach (GameObject Boid in Neighbours)
        {
            AligningVector += Boid.transform.position;

        }
        AligningVector /= Neighbours.Count;


        return AligningVector.normalized;
    }

    Vector3 CohesionVector (List<GameObject> Neighbours)
    {
        if (Neighbours.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 AveragePosition = transform.position;

        foreach (GameObject Boid in Neighbours)
        {
            AveragePosition += Boid.transform.position;
        }
        AveragePosition /= (Neighbours.Count + 1);
        return (AveragePosition - transform.position).normalized;
    }

    Vector3 CalculateBoundsVector()
    {
        var offsetToCenter = BoidManager.transform.position - transform.position;
        if (offsetToCenter.magnitude >= BoidManager.Bounds * 0.9f)
        {
            return offsetToCenter.normalized;
        }
        else
        {
            return Vector3.zero;
        }
    }




}
