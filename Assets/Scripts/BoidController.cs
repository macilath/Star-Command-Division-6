using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Attached to each boid with a spherical collider
// http://virtualmore.org/wiki/index.php?title=SimpleBoids#C.23_Scripts
public class BoidController : MonoBehaviour {

    public float minVelocity = 1;
    public float maxVelocity = 8;
    public int flockSize = 3;

    // Likelihood of boids
    public float centerWeight = 1;
    // Boid alignment
    public float velocityWeight = 1;
    // Likelihood of separation
    public float separationWeight = 1;
    // Follow the leader
    public float followWeight = 1;
    public float randomizeWeight = 1;

    public Boids prefab;
    // target for boids, can be a leader for them to follow
    public Transform target;

    internal Vector3 flockCenter;
    internal Vector3 flockVelocity;

    public List<Boids> boidList = new List<Boids>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < flockSize; i++)
        {
            Boids boid = Instantiate(prefab, transform.position, transform.rotation) as Boids;
            boid.transform.parent = transform;
            boid.transform.localPosition = new Vector3(Random.value * collider.bounds.size.x, Random.value * collider.bounds.size.y, 0) - collider.bounds.extents;
            boid.controller = this;
            boidList.Add(boid); 
        }
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;
        foreach (Boids b in boidList)
        {
            center += b.transform.localPosition;
            velocity += b.rigidbody.velocity;
        }
        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
	}
}
