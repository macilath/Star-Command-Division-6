using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public float detectionRadius;           // size of detection circle
    public float fieldOfViewAngle;           // Number of degrees, centred on up, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.

    private float angle;

    private Vector3 directionFromPlayer;
    private Vector3 pos;

    void Start()
    {
        transform.localScale = new Vector3(detectionRadius, detectionRadius, 1);
        playerInSight = false;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            // Create a vector from the enemy to the player and store the angle between it and forward.
            directionFromPlayer = other.transform.position - transform.position;
            angle = Vector3.Angle(directionFromPlayer, transform.up);

            if (angle < fieldOfViewAngle)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionFromPlayer.normalized, out hit, detectionRadius/2))
                {
                    Debug.DrawRay(transform.position, directionFromPlayer.normalized * (detectionRadius/2));
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject.tag == "Ship")
                    {
                        // ... the player is in sight.
                        playerInSight = true;
                    }
                }
            }
            else
            {
                playerInSight = false;
            }

            if (playerInSight)
            {
                Debug.Log(string.Format("Angle: {0}, In Sight", angle));
            }
            else
            {
                Debug.Log(string.Format("Angle: {0}, NOT IN SIGHT", angle));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerInSight = false;
    }
}