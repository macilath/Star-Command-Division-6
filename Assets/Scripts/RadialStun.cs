using UnityEngine;
using System.Collections;

public class RadialStun : MonoBehaviour {

	private float maxRadius;
	private float scaleSpeed;
	private int stunTime;
	private string enemyTag;
	// Use this for initialization

	void Awake()
	{
		maxRadius = 100f;
		scaleSpeed = 2f;
		stunTime = 5000;
		enemyTag = "EnemyShip";
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).deactivate(stunTime);
        }
    }
}
