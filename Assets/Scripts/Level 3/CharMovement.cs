using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
    public float speed = 0.25F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
	private tk2dSpriteAnimator anim;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
		anim = GetComponentInChildren<tk2dSpriteAnimator>();
    }

    void Update()
    {
        float h_axis = Input.GetAxisRaw("Horizontal");
        float v_axis = Input.GetAxisRaw("Vertical");

        // Locking y position
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;

        if (controller != null)
        {
            moveDirection = new Vector3(h_axis, 0, v_axis);
            controller.Move(moveDirection * speed);
			
			if(moveDirection != Vector3.zero)
			{
				Debug.Log("got here");
				anim.Play("HeroWalk");
			}
			else
			{
				anim.Play("HeroIdle");	
			}
        }
    }
}