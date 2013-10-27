using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float Timer;
	private float startTime;
	// Use this for initialization
	void Start () {
		//Debug.Log("I'm attached to "+this.gameObject.transform.name);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > Timer){
			Destroy(this.gameObject);	
		}
	}
}
