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
        shipHealth = 100;
        maxHealth = 100;
        hasTarget = false;
        facingTarget = true;
        targetIsEnemy = false;
	}
	
	void Update () {
        checkHealth();
        Vector3 shipPosition = playerShip.transform.position;

        getShipSelected(shipPosition);
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

    protected override void getShipSelected(Vector3 shipPosition)
    {
        Vector3 camPos = Camera.main.WorldToScreenPoint(shipPosition);
        if (Input.GetMouseButtonUp(0))
        {
            //camPos.y = Mouse.InverseMouseY(camPos.y);

            // if the user simply clicks then we will want to be able to select that ship
            // If the user simply clicks and doesn't drag, the selection box will be smaller than this
            if (Mouse.selection.width <= 10 && Mouse.selection.height <= 10)
            {
                Rect boundingRect = new Rect(Input.mousePosition.x - 75, Input.mousePosition.y - 75, 150, 150);
                if (boundingRect.Contains(camPos))
                {
                    Debug.Log("Found object: " + this.name);
                    isSelected = true;
                }
                else
                {
                    Debug.Log("Deselected: " + this.name);
                    isSelected = false;
                }
            }
            else
            {
                if (Mouse.selection.Contains(camPos))
                {
                    Debug.Log("Found object: " + this.name);
                    isSelected = true;
                }
                else
                {
                    Debug.Log("Deselected: " + this.name);
                    isSelected = false;
                }
            }
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

    protected override float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
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

    protected override void rotate(Vector3 shipPosition)
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

    protected override void move(Vector3 shipPosition)
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

    protected override void checkHealth()
    {
        if(shipHealth <= 0)
        {
            GameObject Explosion = (GameObject)Resources.Load("ShipExplode1");
            Instantiate(Explosion, playerShip.transform.position, Quaternion.identity);
            Destroy(playerShip);
        }
    }

    public override void takeDamage(int damage)
    {

        shipHealth -= damage;
    }

    protected override void fireWeapons()
    {

    }
}
