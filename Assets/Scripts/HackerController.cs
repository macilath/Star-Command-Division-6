using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class HackerController : PlayerController {

    //private int hackedStations = 0; 

    void Start()
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

    void Update()
    {
        base.Update();
        if (this.shipHealth <= 0)
        {
            manager.hackerAlive = false;
        }
    }

    protected override void fireWeapons()
    {
        UnityEngine.Debug.Log("Ship " + thisShip.name + " has fired.");
        GameObject Projectile = (GameObject)Resources.Load("StunProjectile");
        Vector3 projectile_position = thisShip.transform.position + (thisShip.transform.up * (shipSizeH + 1));
        GameObject projObject = Instantiate(Projectile, projectile_position, thisShip.transform.rotation) as GameObject;

        WeaponController proj = projObject.GetComponent<WeaponController>();
        proj.setEnemyTag("EnemyShip");
        stopwatch.Start();
        //proj.setParent(this.gameObject);
        //TODO: set the target of the projectile
        //proj.setTarget()
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HackStation")
        {
            UnityEngine.Debug.Log("Met hackstation");
            GeneratorController gen = other.gameObject.GetComponent<GeneratorController>();
            gen.startHack();
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HackStation")
        {
            if (hackWatch.ElapsedMilliseconds >= 5000)
            {
                // Set tag to hacked?
                other.gameObject.tag = "Hacked";
                hackWatch.Stop();
                manager.hackedStations++;
                print("Hacked stations: " + manager.hackedStations);
            }
        }
    }*/
    
    void OnTriggerLeave(Collider other)
    {
        if (other.gameObject.tag == "HackStation")
        {
            GeneratorController gen = other.gameObject.GetComponent<GeneratorController>();
            gen.stopHack();
        }
    }
}