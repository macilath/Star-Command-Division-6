using UnityEngine;
using System.Collections;
using System;

public class EnemyController : UnitController {

    EnemySight vision;
    void Start () {
        thisShip = this.gameObject;
        Electric = (GameObject)Resources.Load("EnemyStun");
        vision = thisShip.GetComponentInChildren<EnemySight>();
        targetDest = thisShip.transform.position;
        isSelected = false;
        shipSpeed = 95;
        shipAccel = 3;
        shipSizeH = 3f;
        shipSizeW = 3f;
        shipRotSpeed = 10f;
        shipHealth = 100;
        maxHealth = 100;
        fireInterval = 800;
        hasTarget = false;
        facingTarget = true;
        targetIsEnemy = false;
    }
    
    void Update () {
        checkHealth();
        Vector3 shipPosition = thisShip.transform.position;
        if(isActive)
        {
            if (vision.playerInSight)
            {
                manager.alertStopwatch.Start();
            }
            if (vision.sightingExists)
            {
                setTarget();
            }
            if (hasTarget && !facingTarget)
            {
                rotate(shipPosition);
            }
            if (hasTarget && facingTarget)
            {
                move(shipPosition);
            }
            if(shipCanFire())
            {
                checkShoot();
            }
        }
        else
        {
            checkStun();
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

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "PlayerShip")
        {
            vision.previousSighting = other.gameObject.transform.position;
            vision.sightingExists = true;
            setTarget();
        }
    }

    //TODO: have ship decide whether target is an object to fire on or just a destination
    public override void setTarget()
    {
        // Assign movement orders to ship
        targetDest = vision.previousSighting;
        facingTarget = false;
        hasTarget = true;
        vision.sightingExists = false;
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
        stopwatch.Start();
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
