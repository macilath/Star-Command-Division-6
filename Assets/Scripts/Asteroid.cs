using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	public GameObject Explosion;
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
			Instantiate(Explosion, other.transform.position, Quaternion.identity);
			Destroy(other.gameObject);
		}
    }
}
