using UnityEngine;
using System.Collections;
using System;

public class EnemyController : UnitController {

    EnemySight vision;
    EnemyFarSight farVis;
    SettingsManager settingManager;

    void Start () {
        thisShip = this.gameObject;
        Electric = (GameObject)Resources.Load("EnemyStun");
        Afterburn = (GameObject)Resources.Load("Afterburner");
        burnFull = "EnemyAfterburnFull";
        burnHalf = "EnemyAfterburnHalf";
        vision = thisShip.GetComponentInChildren<EnemySight>();
        farVis = thisShip.GetComponentInChildren<EnemyFarSight>();
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
        settingManager = GameObject.Find("SettingsManager").GetComponent<SettingsManager>();
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
            switch(manager.difficultyLevel)
            {
                case 1:
                {
                    if (vision.sightingExists)
                    {
                        setTarget();
                    }
                    break;
                }
                case 2:
                {
                    if (farVis != null && farVis.sightingExists)
                    {
                        setTarget();
                    }
                    break;
                }
            }
            if ( (hasTarget && !facingTarget) )
            {
                rotate(shipPosition, targetDest);
            }
            if( (!vision.facingPlayer && vision.playerInSight) )
            {
                rotate(shipPosition, vision.playerPos);
            }
            if (hasTarget && facingTarget)
            {
                move(shipPosition);
            }
            if(shipCanFire() && vision.facingPlayer && vision.playerInSight)
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

    public override void getShipSelected(bool selected)
    {
        throw new NotImplementedException();
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
        prevTravel = this.rigidbody.velocity;
        this.rigidbody.AddForce( -1 * (prevTravel / shipAccel) );
        // Assign movement orders to ship
        switch(settingManager.difficultyLevel)
        {
            case 1:
            {
                targetDest = vision.previousSighting;
                vision.sightingExists = false;
                break;
            }
            case 2:
            {
                targetDest = farVis.previousSighting;
                farVis.sightingExists = false;
                break;
            }
        }
        facingTarget = false;
        hasTarget = true;
    }

    public override void takeDamage(int damage)
    {

        shipHealth -= damage;
    }

    protected override void fireWeapons()
    {
        //Debug.Log("Ship " + thisShip.name + " has fired.");
        GameObject Projectile = (GameObject)Resources.Load("Projectile");
        Vector3 projectile_position = thisShip.transform.position + (thisShip.transform.up * (shipSizeH + 1));
        GameObject projObject = Instantiate(Projectile, projectile_position, thisShip.transform.rotation) as GameObject;
        WeaponController proj = projObject.GetComponent<WeaponController>();
        proj.setEnemyTag("PlayerShip");
        stopwatch.Start();
    }

    private void checkShoot()
    {
        if(vision.playerInSight)
        {
            fireWeapons();
        }
    }

    protected override void checkHealth()
    {
        if( shipHealth <= 0 )
        {
            manager.EnemyShips.Remove(thisShip);
        }
        base.checkHealth();
    }
}
