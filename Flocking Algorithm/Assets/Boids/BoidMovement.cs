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
    public float MaxNeighbouringDistance;
    public float AvoidingWeight;
    public float AligningWeight;

    public Color[] colors;

    void Start()
    {
        //set random velocity
        Heading = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Heading = Heading.normalized;

        //set random colour 
        SpriteRenderer renderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        renderer.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        Heading = CalculateNewHeading();
        transform.rotation = Quaternion.LookRotation(Heading);
        transform.position += transform.forward * Speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > Bounds || Mathf.Abs(transform.position.z) > Bounds)
        {
            RespawnBoid();
        }

    }

    List<GameObject> GetNeighbours(float DistanceWeight)
    {
        //allocation of memory and deallocation is quite inefficient
        List<GameObject> Neighbours = new List<GameObject>();

        foreach (GameObject Boid in BoidManager.BoidsList)
        {
            if (BoidInFOV(Boid.transform.position, MaxFOV) && BoidInRange(Boid.transform.position, MaxNeighbouringDistance) && Boid != this.gameObject)
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
        Vector3 NewHeading = transform.forward;
        NewHeading += AvoidingVector(GetNeighbours(5f))  * AvoidingWeight;
        NewHeading += AlignmentVector(GetNeighbours(7f)) * AligningWeight;

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

    void RespawnBoid()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, Mathf.Infinity, BoundsLayer))
        {
            transform.position -= transform.forward * (hit.distance - 1f);
        }

    }



}
