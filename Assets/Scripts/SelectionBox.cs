using UnityEngine;
using System.Collections;

public class SelectionBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Found something");
        if (other.gameObject.tag == "PlayerShip")
        {
            //Debug.Log("It's a ship!");
            other.gameObject.GetComponent<PlayerController>().getShipSelected(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entering thing");
        if (other.gameObject.tag == "PlayerShip")
        {
            //Debug.Log("Found me a ship");
        }
    }
}
