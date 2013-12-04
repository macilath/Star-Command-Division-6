using UnityEngine;
using System.Collections;

public class SpaceStationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 torque = new Vector3(0, 0, 5);
		this.gameObject.rigidbody.AddTorque(torque);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
