using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    private Camera cam;
    public Transform targetLoc;
    private Vector3 targetPos;
    private Vector3 screenMiddle; 

	// Use this for initialization
	void Start () {
	    cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        this.transform.position = cam.transform.position; 
	}
	
	// Update is called once per frame
    void Update()
    {
        Transform dest = GameObject.Find("SpaceStation").transform;
        Vector3 targetPos = new Vector3(dest.position.x, dest.position.y, 0);
        this.transform.LookAt(targetPos, Vector3.up);
        //this.transform.rotation = transform.rotation * Quaternion.Euler(90, 90, 0);
        //this.transform.rotation.SetAxisAngle(new Vector3(1, 0, 0), 0);
        //this.transform.rotation.SetAxisAngle(new Vector3(0, 1, 0), 0); 
    }
}
