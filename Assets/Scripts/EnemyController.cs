using UnityEngine;
using System.Collections;
using System;

public class EnemyController : UnitController {

    void Start () {
        // For level 1 we are just looking for 1 ship
        thisShip = this.gameObject;
        targetDest = thisShip.transform.position;
        isSelected = false;
        shipSpeed = 30;
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
        Vector3 shipPosition = thisShip.transform.position;

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
        checkShoot();
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
                    isSelected = false;
                }
            }
        }
    }

    //TODO: have ship decide whether target is an object to fire on or just a destination
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
            //thisShip.rigidbody.AddRelativeForce(-shipVelocity * thisShip.rigidbody.mass);
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
                thisShip.rigidbody.AddRelativeForce(forceVector);
                return;
            }
        }*/

        this.rigidbody.AddForce(forceVector);
    }

    public override void takeDamage(int damage)
    {

        shipHealth -= damage;
    }

    protected override void fireWeapons()
    {
        Debug.Log("Ship " + thisShip.name + " has fired.");
        GameObject Projectile = (GameObject)Resources.Load("Projectile");
        Vector3 projectile_position = thisShip.transform.position + (thisShip.transform.up * (shipSizeH + 1));
        GameObject projObject = Instantiate(Projectile, projectile_position, thisShip.transform.rotation) as GameObject;
        WeaponController proj = projObject.GetComponent<WeaponController>();
        proj.setEnemyTag("PlayerShip");

        //TODO: set the target of the projectile
        //proj.setTarget()
    }

    private void checkShoot()
    {
        //temporary, until intelligent firing is in place
        /*if(Input.GetKeyDown("space"))
        {
            fireWeapons();
        }*/
        EnemySight vision = thisShip.GetComponentInChildren<EnemySight>();
        if(vision.playerInSight)
        {
            fireWeapons();
        }
    }
}
