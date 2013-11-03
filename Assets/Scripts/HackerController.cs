using UnityEngine;
using System.Collections;
using System;

public class HackerController : PlayerController {

    Start()
    {
        base.Start();
        shipSpeed = 25;
        shipAccel = 4;
        shipSizeH = 3f;
        shipSizeW = 3f;
        shipRotSpeed = 10f;
        maxHealth = 150;
        shipHealth = 150;
    }

    Update()
    {
        base.Update();
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
        //proj.setParent(this.gameObject);
        //TODO: set the target of the projectile
        //proj.setTarget()
    }
}