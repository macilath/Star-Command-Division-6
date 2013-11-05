using UnityEngine;
using System.Collections;
using System;

public class StunController : WeaponController {

    void Start()
    {
        base.Start();
        weaponDamage = 2000;
        weaponSpeed = 400;
    }

    void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).deactivate(weaponDamage);
        }
        else if(other.gameObject.tag == "Asteroid")
        {
            Vector3 point_of_contact = (other.contacts[0]).point;
            other.rigidbody.AddForceAtPosition((this.rigidbody.velocity) * 5, point_of_contact);
        }
        Destroy(weapon);
    }
}