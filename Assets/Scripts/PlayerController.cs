using UnityEngine;
using System.Collections;
using System;

public class PlayerController : UnitController {

    /*
     * For Player Unit Selection:
     * We can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     */

	protected void Start () {
		thisShip = this.gameObject;
        targetDest = thisShip.transform.position;
        Afterburn = (GameObject)Resources.Load("Afterburner");
        burnFull = "FriendlyAfterburnFull";
        burnHalf = "FriendlyAfterburnHalf";
	    isSelected = false;
	    shipSpeed = 100;
	    shipAccel = 3;
        shipSizeH = 3f;
        shipSizeW = 3f;
        shipRotSpeed = 10f;
        maxHealth = 100;
        shipHealth = 100;
        fireInterval = 750;
        hasTarget = false;
        facingTarget = true;
        targetIsEnemy = false;
	}
	
	protected void Update () {
        checkHealth();
        if (thisShip == null) { Debug.Break(); }
        Vector3 shipPosition = thisShip.transform.position;
        if(isActive)
        {
            if (isSelected)
            {
                setTarget();
                if(shipCanFire())
                {
                    checkShoot();
                }
            }
            if (hasTarget && !facingTarget)
            {
                rotate(shipPosition);
            }
            if (hasTarget && facingTarget)
            {
                move(shipPosition);
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

    public override void getShipSelected(bool select)
    {
        isSelected = select;
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
        proj.setEnemyTag("EnemyShip");
        stopwatch.Start();
    }

    private void checkShoot()
    {
        if(Input.GetKeyDown("space"))
        {
            fireWeapons();
        }
    }

    protected override void checkHealth()
    {
        if( shipHealth <= 0 )
        {
            manager.KillShip(thisShip);
        }
        base.checkHealth();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpaceStation")
        {
            manager.AddSurvivor(thisShip);
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpaceStation")
        {
            manager.RemoveSurvivor(thisShip);
        }
    }
}
