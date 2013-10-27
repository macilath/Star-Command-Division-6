using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private const int asteroidDamage = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ship" || other.gameObject.tag == "EnemyShip")
		{
			((UnitController)((other.gameObject).GetComponent("UnitController"))).takeDamage(asteroidDamage);
		}
    }
}
