using UnityEngine;
using System.Collections;

public class Safety : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Hostage")
        {
            //Debug.Log("safe");
            GameManager.hostageSafe = true;
        }
    }
}
