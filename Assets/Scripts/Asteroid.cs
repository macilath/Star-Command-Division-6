using UnityEngine;
using System.Collections;
//using System.Random;

public class Asteroid : MonoBehaviour {

	private const int asteroidDamage = 5;
	private System.Random rand = new System.Random();
	private const int maxSpinSpeed = 7;

	// Use this for initialization
	void Start () {
		// set random spin speed
		int spinSpeed = 5;//rand.Next(maxSpinSpeed);
		Vector3 torque = new Vector3(0, 0, spinSpeed);
		this.gameObject.rigidbody.AddTorque(torque);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.tag == "PlayerShip" || other.gameObject.tag == "EnemyShip")
		{
		    ((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(asteroidDamage);
		}
    }
}
