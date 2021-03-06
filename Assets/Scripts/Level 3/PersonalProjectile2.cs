﻿﻿using UnityEngine;
using System.Collections;
using System;

public class PersonalProjectile2 : MonoBehaviour
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
        Init();
        manager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        weapon = this.gameObject;
        initialPosition = weapon.transform.position;
        kick();
    }

    protected virtual void Init()
    {
        weaponDamage = 25;
        weaponRange = 1000;
        weaponSpeed = 1000;
    }

    protected void kick()
    {
        Vector3 forceVector = weapon.transform.up * weaponSpeed;
        //Debug.Log("projectile force vector: " + forceVector);
        weapon.rigidbody.AddForce(forceVector);
    }

    public void setEnemyTag(string tag)
    {
        enemyTag = tag;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {	
            //Debug.Log("HIT PLAYER");
            //((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(weaponDamage);
            Destroy(weapon);
            Destroy(other.gameObject);
        }
		else if (other.gameObject.tag == "Hostage")
		{
			//Debug.Log("HIT Hostage");
            //((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(weaponDamage);
            Destroy(weapon);
            Destroy(other.gameObject);

            GameManager.hostageAlive = false;
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
}
