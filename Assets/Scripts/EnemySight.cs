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
        playerInSight = false;
    }

    void Update()
    {
        Debug.DrawLine(transform.up, transform.up * 2);
        Debug.DrawLine(directionFromPlayer, transform.up);
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
                playerInSight = true;
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