using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public Camera camera;
	public float moveSpeed = 0.2f;
	
	void Start()
	{
		camera = GetComponent<Camera>();
	}
	
	void FixedUpdate()
	{
		if(camera.transform.position.x < 10 && Input.GetAxisRaw("Horizontal") > 0)
		{
			camera.transform.Translate(new Vector3((Input.GetAxisRaw("Horizontal") * moveSpeed), 0, 0));
		}
		
		if(camera.transform.position.x > - 10 && Input.GetAxisRaw("Horizontal") < 0)
		{
			camera.transform.Translate(new Vector3((Input.GetAxisRaw("Horizontal") * moveSpeed), 0, 0));
		}
		
		if(camera.transform.position.y < 10 && Input.GetAxisRaw("Vertical") > 0)
		{
			camera.transform.Translate(new Vector3(0, (Input.GetAxisRaw("Vertical") * moveSpeed), 0));
		}
		
		if (camera.transform.position.y > - 10 && Input.GetAxisRaw("Vertical") < 0)
		{
			camera.transform.Translate(new Vector3(0, (Input.GetAxisRaw("Vertical") * moveSpeed), 0));
		}
	}
}
