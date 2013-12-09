using UnityEngine;
using System.Collections;

public class Lv3EnemySight : MonoBehaviour
{
    private float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.
    public Vector3 personalLastSighting;            // Last place this enemy spotted the player.
    
	private NavMeshAgent nav;						// Refernce to the Nav Mesh
    private SphereCollider col;                     // Reference to the sphere collider trigger component.
    private LastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
    private GameObject player;                      // Reference to the player.
    private Vector3 previousSighting;               // Where the player was sighted last frame.
    
    
    void Awake ()
    {
		//nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Lv3Tags.gameController).GetComponent<LastPlayerSighting>();
		
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
    }
    
    
    void Update ()
    {
		if(lastPlayerSighting.position != previousSighting)
		{
			personalLastSighting = lastPlayerSighting.position;	
		}
		
		previousSighting = lastPlayerSighting.position;
		
    }
    

    void OnTriggerStay (Collider other)
    {
        // If the player has entered the trigger sphere...
        if(other.gameObject == player)
        {	
            // By default the player is not in sight.
            playerInSight = false;
            
            // Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            
			
            // If the angle between forward and where the player is, is less than half the angle of view...
            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                
                // ... and if a raycast towards the player hits something...
                if(Physics.Raycast(transform.position + transform.forward, direction.normalized, out hit, col.radius*2))
                {
					 Debug.DrawRay(transform.position, direction.normalized * col.radius*2);
                    // ... and if the raycast hits the player...
                    if(hit.collider.gameObject == player)
                    {
						//Debug.Log("Player in sight!");
                        // ... the player is in sight.
                        playerInSight = true;
                        
                        // Set the last global sighting is the players current position.
                        lastPlayerSighting.position = player.transform.position;
                    }
					else
					{
						//Debug.Log("Player collided but not in sight");
					}
                }
            }
        }
    }
    
    void OnTriggerExit (Collider other)
    {
        // If the player leaves the trigger zone...
        if(other.gameObject == player)
		{
			//Debug.Log("Player left");
            // ... the player is not in sight.
            playerInSight = false;
		}
    }
}