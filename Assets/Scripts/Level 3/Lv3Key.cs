using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    public AudioClip keyGrab;                       // Audioclip to play when the key is picked up.

    private GameObject player;                      // Reference to the player.
    
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag(Lv3Tags.player);
    }
    
    void OnTriggerEnter (Collider other)
    {
        // If the colliding gameobject is the player...
        if(other.gameObject == player)
        {
            // ... play the clip at the position of the key...
            AudioSource.PlayClipAtPoint(keyGrab, transform.position);
            
            // ... the player has a key ...
            player.GetComponent<Lv3Player>().hasKey1 = true;
            
            // ... and destroy this gameobject.
            Destroy(gameObject);
        }
    }
}