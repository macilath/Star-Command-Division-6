using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, UnitController {

    /*
     * For Player Unit Selection:
     * We can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     */

	void Start () {
		// For level 1 we are just looking for 1 ship
		playerShip = GameObject.Find("Ship");
	}
	
	//public Transform target; 
	public GameObject playerShip;
	private bool isSelected = false;
	public static GameManager manager; 
	
	void Update () {	
		Vector3 shipPosition = Camera.main.WorldToScreenPoint(playerShip.transform.position);
		
		if(Input.GetMouseButtonDown(0) && isSelected == false) {
			print("Mouse0 detected");
			print(Input.mousePosition);
			// Create our bounding rectangle
			Rect boundingRect = new Rect(Input.mousePosition.x, Input.mousePosition.y, 10, 10);
			// See if our Ship object is in the bounding rectangle
			if(boundingRect.Contains(shipPosition)){
				print("Found object" + manager.Selectable[0]);
				isSelected = true;
			}
		}
		else isSelected = false;
	}
	
}
