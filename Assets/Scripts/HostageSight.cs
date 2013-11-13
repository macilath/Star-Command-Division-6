﻿using UnityEngine;
using System.Collections;

public class HostageSight : MonoBehaviour {

    public bool followPlayer;
	// Use this for initialization
	void Start () {
        followPlayer = false;
	}
	

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            followPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            followPlayer = false;
        }
    }
}