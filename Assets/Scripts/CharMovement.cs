using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour
{
    public float speed = 6.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        this.rigidbody.AddForce(moveDirection * Time.deltaTime);
    }
}