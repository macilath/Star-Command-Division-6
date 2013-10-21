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
    public float shipRotSpeed = 10f;
    public Vector3 targetDest;
    private bool hasTarget = false;
    private bool facingTarget = true;
    private bool targetIsEnemy = false;

	void Start () {
		// For level 1 we are just looking for 1 ship
		playerShip = this.gameObject;
        targetDest = playerShip.transform.position;
	}
	
	void Update () {
        Vector3 shipPosition = playerShip.transform.position;
		Vector3 shipPositionScreen = Camera.main.WorldToScreenPoint(shipPosition);
        
        getShipSelected(shipPositionScreen);
        setTarget();
        if (hasTarget && !facingTarget)
        {
            rotate(shipPosition);
        }
        if (hasTarget && facingTarget)
        {
            move(shipPosition);
        }
	}

    void getShipSelected(Vector3 shipPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isSelected == false)
            {
                Debug.Log("Mouse0 detected");
                Debug.Log(Input.mousePosition);
                // Create our bounding rectangle - the size of which still needs some testing/debugging
                Rect boundingRect = new Rect(Input.mousePosition.x - 75, Input.mousePosition.y - 75, 150, 150);
                // See if our Ship object is in the bounding rectangle
                if (boundingRect.Contains(shipPosition))
                {
                    Debug.Log("Found object");
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
                Debug.Log("Orders: GOTO " + targetDest);
            }
            else if (hasTarget == true && Input.GetMouseButtonDown(1) && isSelected == true)
            {
                playerShip.rigidbody.velocity = new Vector3(0, 0, 0);
                targetDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetDest.z = 0.0f;
                Debug.Log("New Orders: GOTO " + targetDest);
            }
            playerShip.rigidbody.angularVelocity = Vector3.zero;
            facingTarget = false;
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        
        if (dir > 0f) {
            return 1f;
        } else if (dir < 0f) {
            return -1f;
        } else {
            return 0f;
        }
    }

    void rotate(Vector3 shipPosition)
    {
        Vector3 toTarget = targetDest - shipPosition;
        float shipAngle = playerShip.transform.rotation.eulerAngles.z;
        float targetAngle = Vector3.Angle(Vector3.up, toTarget) * AngleDir(Vector3.up, toTarget, Vector3.forward);
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        float rotationAngle = targetAngle - shipAngle;
        //Debug.Log("Rotate from " + shipAngle + " to " + targetAngle);
        //Debug.Log(rotationAngle + " degrees");
        playerShip.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        facingTarget = true;
        //TODO: make rotation fluid instead of instant
    }

    void move(Vector3 shipPosition)
    {
        // Move ship
        Vector3 forceVector = (targetDest - shipPosition);
        forceVector.Normalize();
        Vector3 shipVelocity = playerShip.rigidbody.velocity;

        Rect boundingRect = new Rect(shipPosition.x - (shipSizeW/2), shipPosition.y - (shipSizeH/2), shipSizeW, shipSizeH);
        //Debug.Log(shipPosition - targetDest);
        if (hasTarget && boundingRect.Contains(targetDest))
        {
            //playerShip.rigidbody.AddRelativeForce(-shipVelocity * playerShip.rigidbody.mass);
            playerShip.rigidbody.velocity = Vector3.zero;
            playerShip.rigidbody.angularVelocity = Vector3.zero;
            Debug.Log("Destination Reached.");
            hasTarget = false;
            return;
        }

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
        /*else //TODO: use this idea to have ship slow down at destination instead of just stop instantly
        {
            if (shipVelocity.sqrMagnitude > 0)
            {
                forceVector = new Vector3(0, -1, 0);
                forceVector *= shipAccel;
                playerShip.rigidbody.AddRelativeForce(forceVector);
                return;
            }
        }*/

        playerShip.rigidbody.AddForce(forceVector);
    }
}
