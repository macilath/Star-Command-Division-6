using UnityEngine;
using System.Collections;

public class Lv3Player : MonoBehaviour {

	private Vector3 lookRotationPoint;
	
    void Update () 
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookRotationPoint = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(lookRotationPoint.normalized, transform.forward);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
