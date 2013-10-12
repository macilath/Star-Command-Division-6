using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, UnitController {

    /*
     * For Player Unit Selection:
     * We can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     */
	public int shipSpeed = 10;
	public int shipAccel = 3;

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
		
		Debug.Log(string.Format("({0}, {1}, {2})",shipPosition.x, shipPosition.y, shipPosition.z));

        if (Input.GetMouseButtonDown(0)) {
            if (isSelected == false) {
                print("Mouse0 detected");
                print(Input.mousePosition);
                // Create our bounding rectangle - the size of which still needs some testing/debugging
                Rect boundingRect = new Rect(Input.mousePosition.x - 75, Input.mousePosition.y - 75, 150, 150);
                // See if our Ship object is in the bounding rectangle
                if (boundingRect.Contains(shipPosition)) {
                    print("Found object");
                    isSelected = true;
                }
            }
            else isSelected = false; // Clear out previous selection
        }

        // Assign movement orders to ship
        if (Input.GetMouseButtonDown(1) && isSelected == true) {
            print("Orders: GOTO " + Input.mousePosition);
			// Later we need to detect if there is an enemy at the coordinates we just chose
			// If so, follow that enemy and attack (since enemies will continue to move)
			Vector3 destination = Input.mousePosition; 
			//transform.position = Vector3.Lerp(shipPosition, destination, 0.2f);
			destination.z = 0; 
			transform.position = destination;
        }
	}
	
}
