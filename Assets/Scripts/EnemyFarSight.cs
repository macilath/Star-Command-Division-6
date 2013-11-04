using UnityEngine;
using System.Collections;

public class EnemyFarSight : MonoBehaviour {

	public float detectionRadius;           // size of detection circle
    public float detectionScale = 20;
    private float fieldOfViewAngle;           // Number of degrees, centred on up, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.

    private float angle;

    private Vector3 directionFromPlayer;
    private Vector3 pos;

    public Vector3 previousSighting;
    public bool sightingExists;

    void Start()
    {
        fieldOfViewAngle = 60;
        detectionRadius = detectionScale * transform.localScale.x/2;
        playerInSight = false;
        sightingExists = false;
        previousSighting = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            // Create a vector from the enemy to the player and store the angle between it and forward.
            directionFromPlayer = other.transform.position - transform.position;
            angle = Vector3.Angle(directionFromPlayer, transform.up);

            if (angle < fieldOfViewAngle)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, directionFromPlayer.normalized, out hit, detectionRadius))
                {
                    Debug.DrawRay(transform.position, directionFromPlayer.normalized * (detectionRadius));
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject.tag == "PlayerShip")
                    {
                        // ... the player is in sight.
                        playerInSight = true;
                        previousSighting = hit.collider.gameObject.transform.position;
                    }
                }
            }
            else
            {
                playerInSight = false;
            }
			
			if (playerInSight)
	        {
	            sightingExists = true;
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
}
