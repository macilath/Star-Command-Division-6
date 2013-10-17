using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour, UnitController {

    /*
     * For Player Unit Selection:
     * We can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     */
	//public Transform target; 
	public GameObject playerShip;
	private bool isSelected = false;
	public static GameManager manager; 
	public int shipSpeed = 10;
	public int shipAccel = 3;
    public float shipSizeH = 3f;
    public float shipSizeW = 3f;
    public float shipRotSpeed = 1f;
    public Vector3 targetDest;
    private bool hasTarget = false;

	void Start () {
		// For level 1 we are just looking for 1 ship
		playerShip = GameObject.Find("Ship");
        targetDest = playerShip.transform.position;
	}
	
	void Update () {
        Vector3 shipPosition = playerShip.transform.position;
        Vector3 shipRotation = playerShip.transform.rotation;
		Vector3 shipPositionScreen = Camera.main.WorldToScreenPoint(shipPosition);
        
        getShipSelected(shipPositionScreen);
        setTarget();
        if (hasTarget)
        {
            rotate(shipRotation, shipPosition);
        }
        move(shipPosition);
	}

    void getShipSelected(Vector3 shipPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isSelected == false)
            {
                print("Mouse0 detected");
                print(Input.mousePosition);
                // Create our bounding rectangle - the size of which still needs some testing/debugging
                Rect boundingRect = new Rect(Input.mousePosition.x - 75, Input.mousePosition.y - 75, 150, 150);
                // See if our Ship object is in the bounding rectangle
                if (boundingRect.Contains(shipPosition))
                {
                    print("Found object");
                    isSelected = true;
                }
            }
            else isSelected = false; // Clear out previous selection
        }
    }

    void setTarget()
    {
        // Assign movement orders to ship
        if (Input.GetMouseButtonDown(1) && isSelected == true)
        {
            if (hasTarget == false)
            {
                hasTarget = true;
                targetDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetDest.z = 0.0f;
                print("Orders: GOTO " + targetDest);
            }
            else if (hasTarget == true && Input.GetMouseButtonDown(1) && isSelected == true)
            {
                playerShip.rigidbody.velocity = new Vector3(0, 0, 0);
                targetDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetDest.z = 0.0f;
                Debug.Log("New Orders: GOTO " + targetDest);
            }
        }
    }

    void rotate(Vector3 shipRotation, Vector3 shipPosition)
    {
        shipRotation.Normalize();
        Vector3 toTarget = targetDest - shipPosition;
        toTarget.Normalize();
        float angle = Vector3.Angle(shipRotation, toTarget);
        if (angle == 0)
        {
            playerShip.rigidbody.angularVelocity = 0;
            return;
        }
        else if (angle <  0)
        {
            Vector3 torque = new Vector3(0, 0, -1);
            playerShip.rigidbody.addTorque(torque * shipRotSpeed);
            return;
        }
        else
        {
            Vector3 torque = new Vector3(0, 0, 1);
            playerShip.rigidbody.addTorque(torque * shipRotSpeed);
            return;
        }
    }

    void move(Vector3 shipPosition)
    {
        // Move ship
        Vector3 forceVector = (targetDest - shipPosition);
        forceVector.Normalize();
        Vector3 shipVelocity = playerShip.rigidbody.velocity;

        Rect boundingRect = new Rect(shipPosition.x - (shipSizeW/2), shipPosition.y - (shipSizeH/2), shipSizeW, shipSizeH);
        Debug.Log(shipPosition - targetDest);
        if (hasTarget && boundingRect.Contains(targetDest))
        {
            //playerShip.rigidbody.AddRelativeForce(-shipVelocity * playerShip.rigidbody.mass);
            playerShip.rigidbody.velocity = new Vector3(0, 0, 0);    //rigidbody.velocity = new Vector3(0, 0, 0);
            Debug.Log("Destination Reached.");
            hasTarget = false;
            return;
        }
        /*
        if (targetDest == shipPosition)
        {
           
            playerShip.rigidbody.velocity = new Vector3(0, 0, 0);
            Debug.Log("Destination Reached.");
            return;
        }
        */

        //TODO: change this to compare vectors using cosine to ensure ship is always trying to move to targetDest
        if (hasTarget)
        {
            if (shipVelocity.sqrMagnitude < shipSpeed)
            {
                forceVector = shipVelocity + (forceVector * shipAccel);
            }
            else
            {
                forceVector = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (shipVelocity.sqrMagnitude > 0)
            {
                forceVector = new Vector3(0, -1, 0);
                forceVector *= shipAccel;
                playerShip.rigidbody.AddRelativeForce(forceVector);
                return;
            }
        }

        playerShip.rigidbody.AddForce(forceVector);
    }
}
