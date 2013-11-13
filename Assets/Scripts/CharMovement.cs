using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
    public float speed = 1.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
		float x_pos = this.transform.position.x;
        float y_pos = this.transform.position.y;
        float h_axis = Input.GetAxisRaw("Horizontal");
        float v_axis = Input.GetAxisRaw("Vertical");
		
        float mouse_x = Input.mousePosition.x;
        float mouse_y = Input.mousePosition.z;
        if(h_axis != 0)
        {
            Vector3 movement;
            if(h_axis != 0)
            {
                movement = new Vector3((Input.GetAxisRaw("Horizontal") * speed), 0, 0);
            }
            else
            {
                movement = new Vector3(speed, 0, 0);
            }
            
            this.transform.Translate(movement);
        }
        if(v_axis != 0)
        {
            Vector3 movement;
            if(v_axis != 0)
            {
                movement = new Vector3(0, 0, (Input.GetAxisRaw("Vertical") * speed));
            }
            else
            {
                movement = new Vector3(0, 0, speed);
            }

            this.transform.Translate(movement);
        }
    }
}