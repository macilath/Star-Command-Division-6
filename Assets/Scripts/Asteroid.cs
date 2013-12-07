using UnityEngine;
using System.Collections;
//using System.Random;

public class Asteroid : MonoBehaviour {

	private const int asteroidDamage = 5;
	private const int maxSpinSpeed = 500;
	public float currentSpinSpeed = 0;
	private GameManager manager;
	private System.Random rand;

	public void Awake()
    {
        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();
        rand = manager.rand;
    }

	// Use this for initialization
	void Start () {
		// set random spin speed
		int neg = rand.Next(0, 3);
		int spinSpeed = 0;
		if(neg == 0)
			spinSpeed = rand.Next(maxSpinSpeed);
		else
			spinSpeed = -1 * rand.Next(maxSpinSpeed);
		Vector3 torque = new Vector3(0, 0, spinSpeed);
		this.gameObject.rigidbody.AddTorque(torque);
	}
	
	// Update is called once per frame
	void Update () {
		currentSpinSpeed = this.gameObject.rigidbody.angularVelocity.z;
	}
	
	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.tag == "PlayerShip" || other.gameObject.tag == "EnemyShip")
		{
		    ((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(asteroidDamage);
		}
    }
}
