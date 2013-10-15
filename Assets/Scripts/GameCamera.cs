using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public Camera camera;
	public float moveSpeed = 0.2f;
    public float bounds = 10;
    private GameObject nebula;
    private GameObject dust;
    private int nebulaMoveFraction = 4;
    private int dustMoveFraction = 2;
	
	void Start()
	{
		camera = GetComponent<Camera>();
        nebula = GameObject.Find("Nebula");
        dust = GameObject.Find("SpaceDust");
	}
	
	void FixedUpdate()
	{
        float x_pos = camera.transform.position.x;
        float y_pos = camera.transform.position.y;
        float h_axis = Input.GetAxisRaw("Horizontal");
        float v_axis = Input.GetAxisRaw("Vertical");

        while(x_pos > bounds)
        {
            x_pos = camera.transform.position.x;
            Vector3 movement = new Vector3(bounds - x_pos, 0, 0);
            camera.transform.Translate(movement);
        }
        while(x_pos < -bounds)
        {
            x_pos = camera.transform.position.x;
            Vector3 movement = new Vector3(-bounds - x_pos, 0, 0);
            camera.transform.Translate(movement);
        }
        while(y_pos > bounds)
        {
            y_pos = camera.transform.position.y;
            Vector3 movement = new Vector3(0, bounds - y_pos, 0);
            camera.transform.Translate(movement);
        }
        while(y_pos < -bounds)
        {
            y_pos = camera.transform.position.y;
            Vector3 movement = new Vector3(0, -bounds - y_pos, 0);
            camera.transform.Translate(movement);
        }

        if(h_axis != 0)
        {
            Vector3 movement = new Vector3((Input.GetAxisRaw("Horizontal") * moveSpeed), 0, 0);
            camera.transform.Translate(movement);
            if(x_pos > -bounds && x_pos < bounds)
            {
                nebula.transform.Translate(movement / nebulaMoveFraction);
                dust.transform.Translate(movement / dustMoveFraction);
            }
        }
        if(v_axis != 0)
        {
            Vector3 movement = new Vector3(0, (Input.GetAxisRaw("Vertical") * moveSpeed), 0);
            camera.transform.Translate(movement);
            if(y_pos > -bounds && y_pos < bounds)
            {
                nebula.transform.Translate(movement / nebulaMoveFraction);
                dust.transform.Translate(movement / dustMoveFraction);
            }
        }
	}
}
