using UnityEngine;
using System.Collections;
using System;

public class PersonalProjectile : MonoBehaviour
{

    protected int weaponDamage;
    protected int weaponRange;
    protected int weaponSpeed;
    protected string enemyTag;

    protected Vector3 initialPosition;

    protected GameObject weapon;

    protected static GameManager manager;

    protected void Start()
    {
        weaponDamage = 25;
        weaponRange = 1000;
        weaponSpeed = 500;
        enemyTag = Lv3Tags.player;

        manager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        weapon = this.gameObject;
        initialPosition = weapon.transform.position;
        kick();
    }

    protected void Update()
    {
        checkBounds();
    }

    protected void kick()
    {
        Vector3 forceVector = weapon.transform.forward * weaponSpeed;
        Debug.Log("projectile force vector: " + forceVector);
        weapon.rigidbody.AddForce(forceVector);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(weaponDamage);
            Destroy(weapon);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(weapon);
        }
        else
        {
            //do nothing
        }
    }

    protected void checkBounds()
    {
        if (weapon != null)
        {
            float fun = Math.Abs(Vector3.Distance(initialPosition, weapon.transform.position));
            //Debug.Log("distance: " + fun); 
            if (fun > weaponRange)
            {
                Destroy(weapon);
            }
        }
    }
}
