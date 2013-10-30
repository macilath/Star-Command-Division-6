using UnityEngine;
using System.Collections;

public class Boids : MonoBehaviour {

    internal BoidController controller;
    IEnumerator Start()
    {
        while (true)
        {
            if (controller)
            {
                Vector3 relativePos = steer() * Time.deltaTime;
                rigidbody.velocity = steer() * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(relativePos);
                
                // enforce maxium and minimum speed
                float speed = rigidbody.velocity.magnitude;
                if (speed > controller.maxVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.maxVelocity;
                }
                else if (speed < controller.minVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * controller.minVelocity;
                }
                float waitTime = Random.Range(0.3f, 0.5f);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    Vector3 steer()
    {
        Vector3 center = controller.flockCenter - transform.localPosition;			// cohesion
        Vector3 velocity = controller.flockVelocity - rigidbody.velocity; 			// alignment
        Vector3 follow = controller.target.localPosition - transform.localPosition; // follow leader
        Vector3 separation = Vector3.zero; 											// separation
        foreach (Boids b in controller.boidList) 
        {
            if (b == this)
            {

            }
            else
            {
                Vector3 relativePos = transform.localPosition - b.transform.localPosition;
                separation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        // 3D space		
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);// randomize

        // 2D space
        //		Vector3 randomize = new Vector3((Random.value * 2) - 1, 0, (Random.value * 2) - 1);		

        randomize.Normalize();

        return (controller.centerWeight * center +
                controller.velocityWeight * velocity +
                controller.separationWeight * separation +
                controller.followWeight * follow +
                controller.randomizeWeight * randomize);
    }

}
