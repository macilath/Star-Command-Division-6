using UnityEngine;
using System.Collections;
using System;

public class StunController : WeaponController {

    void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        weaponDamage = 2000;
        weaponRange = 20;
        weaponSpeed = 500;
        weaponAccel = 0;
        weaponSizeH = 3f;
        weaponSizeW = 0.3f;
    }

    void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).deactivate(weaponDamage);
        }
        else if(other.gameObject.tag == "Asteroid")
        {
            Vector3 point_of_contact = other.ClosestPointOnBounds(weapon.transform.position);
            other.rigidbody.AddForceAtPosition((this.rigidbody.velocity) * 5, point_of_contact);
        }

        if( other.gameObject.tag == "Vision" )
        {
            //do nothing
        }
        else
        {
            Destroy(weapon);
        }
    }
}