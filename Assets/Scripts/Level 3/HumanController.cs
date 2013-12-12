using UnityEngine;
using System.Collections;

public abstract class HumanController : MonoBehaviour {

	protected int health = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
		checkHealth();
	}

	protected void checkHealth()
	{
		if( health <= 0 )
		{
			kill();
		}
	}

	public void takeDamage(int damage)
	{
		health -= damage;
	}

	public abstract void kill();
}
