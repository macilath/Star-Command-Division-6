using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public Camera camera;
	public float moveSpeed = 0.2f;
    public float bounds = 10;
	
	void Start()
	{
		camera = GetComponent<Camera>();
	}
	
	void FixedUpdate()
	{
		/*if(camera.transform.position.x < 10 && Input.GetAxisRaw("Horizontal") > 0)
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
		
		if(camera.transform.position.y > - 10 && Input.GetAxisRaw("Vertical") < 0)
		{
			camera.transform.Translate(new Vector3(0, (Input.GetAxisRaw("Vertical") * moveSpeed), 0));
		}*/

        float x_pos = camera.transform.position.x;
        float y_pos = camera.transform.position.y;
        float h_axis = Input.GetAxisRaw("Horizontal");
        float v_axis = Input.GetAxisRaw("Vertical");

        while(x_pos > bounds)
        {
            x_pos = camera.transform.position.x;
            camera.transform.Translate(new Vector3(bounds - x_pos, 0, 0));
        }
        while(x_pos < -bounds)
        {
            x_pos = camera.transform.position.x;
            camera.transform.Translate(new Vector3(-bounds - x_pos, 0, 0));
        }
        while(y_pos > bounds)
        {
            y_pos = camera.transform.position.y;
            camera.transform.Translate(new Vector3(0, bounds - y_pos, 0));
        }
        while(y_pos < -bounds)
        {
            x_pos = camera.transform.position.x;
            camera.transform.Translate(new Vector3(0, -bounds - y_pos, 0));
        }

        if(h_axis != 0)
        {
            camera.transform.Translate(new Vector3((Input.GetAxisRaw("Horizontal") * moveSpeed), 0, 0));
        }
        if(v_axis != 0)
        {
            camera.transform.Translate(new Vector3(0, (Input.GetAxisRaw("Vertical") * moveSpeed), 0));
        }
	}
}
