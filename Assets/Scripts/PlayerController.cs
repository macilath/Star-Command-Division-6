using UnityEngine;
using System.Collections;
using System;

public class PlayerController : UnitController {

    /*
     * For Player Unit Selection:
     * We can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     */

	//public Transform target; 
	/*public GameObject playerShip;
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
    private bool targetIsEnemy = false;*/

	void Start () {
		// For level 1 we are just looking for 1 ship
		playerShip = this.gameObject;
        targetDest = playerShip.transform.position;
	    isSelected = false;
	    shipSpeed = 10;
	    shipAccel = 3;
        shipSizeH = 3f;
        shipSizeW = 3f;
        shipRotSpeed = 10f;
        hasTarget = false;
        facingTarget = true;
        targetIsEnemy = false;
	}
	
	void Update () {
        Vector3 shipPosition = this.transform.position;

        if (renderer.isVisible && Input.GetMouseButtonUp(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = Mouse.InverseMouseY(camPos.y);

            // if the user simply clicks then we will want to be able to select that ship
            Rect boundingRect = new Rect(Input.mousePosition.x - 75, Input.mousePosition.y - 75, 150, 150);
            
            // If the user simply clicks and doesn't drag, the selection box will be smaller than this
            if (Mouse.selection.width < 150 && Mouse.selection.height < 150)
            {
                isSelected = boundingRect.Contains(camPos);
            }
            else
            {
                isSelected = Mouse.selection.Contains(camPos);
            }
        }

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

    void OnDrawGizmos()
    {
        if (isSelected)
        {
            Gizmos.DrawWireCube(this.renderer.bounds.center, new Vector3(this.renderer.bounds.size.x, this.renderer.bounds.size.y));
        }
    }

    public override void getShipSelected(Vector3 shipPosition)
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

    public override void setTarget()
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
                this.rigidbody.velocity = new Vector3(0, 0, 0);
                targetDest = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetDest.z = 0.0f;
                Debug.Log("New Orders: GOTO " + targetDest);
            }
            this.rigidbody.angularVelocity = Vector3.zero;
            facingTarget = false;
        }
    }

    public override float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
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

    public override void rotate(Vector3 shipPosition)
    {
        Vector3 toTarget = targetDest - shipPosition;
        float shipAngle = this.transform.rotation.eulerAngles.z;
        float targetAngle = Vector3.Angle(Vector3.up, toTarget) * AngleDir(Vector3.up, toTarget, Vector3.forward);
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        float rotationAngle = targetAngle - shipAngle;
        //Debug.Log("Rotate from " + shipAngle + " to " + targetAngle);
        //Debug.Log(rotationAngle + " degrees");
        this.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        facingTarget = true;
        //TODO: make rotation fluid instead of instant
    }

    public override void move(Vector3 shipPosition)
    {
        // Move ship
        Vector3 forceVector = (targetDest - shipPosition);
        forceVector.Normalize();
        Vector3 shipVelocity = this.rigidbody.velocity;

        Rect boundingRect = new Rect(shipPosition.x - (shipSizeW/2), shipPosition.y - (shipSizeH/2), shipSizeW, shipSizeH);
        //Debug.Log(shipPosition - targetDest);
        if (hasTarget && boundingRect.Contains(targetDest))
        {
            //playerShip.rigidbody.AddRelativeForce(-shipVelocity * playerShip.rigidbody.mass);
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
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

        this.rigidbody.AddForce(forceVector);
    }
}
