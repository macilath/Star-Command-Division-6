using UnityEngine;
using System.Collections;

public class HostageSight : MonoBehaviour {

    public bool followPlayer;
	// Use this for initialization
	void Start () {
        followPlayer = false;
	}
	
	void Update()
	{
		//Debug.Log(followPlayer);	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            followPlayer = true;
        }
    }
}
