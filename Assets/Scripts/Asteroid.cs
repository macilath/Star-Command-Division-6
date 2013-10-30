using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private const int asteroidDamage = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionStay(Collision other)
	{
		if(other.gameObject.tag == "PlayerShip" || other.gameObject.tag == "EnemyShip")
		{
            Debug.Log("collision with ship");
			((UnitController)((other.gameObject).GetComponent("UnitController"))).takeDamage(asteroidDamage);
		}
    }
}
