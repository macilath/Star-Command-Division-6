using UnityEngine;
using System.Collections;
using System;

public class WeaponController : MonoBehaviour {

    protected int weaponDamage;
    protected int weaponRange;
    protected int weaponSpeed;
    protected int weaponAccel;
    protected float weaponSizeH;
    protected float weaponSizeW;
    protected bool isCollided;
    protected bool hasTarget;
    protected string enemyTag;

    protected Vector3 initialPosition;

    protected GameObject weapon;
    protected GameObject target;
    protected GameObject parentShip;

    protected void Start () {
        Init();
	    isCollided = false;
	    hasTarget = false;
	    weapon = this.gameObject;
        initialPosition = weapon.transform.position;
        kick();
        //for debugging, should be determined by firing ship
        //enemyTag = "EnemyShip";
    }

    protected virtual void Init()
    {
        weaponDamage = 25;
        weaponRange = 30;
        weaponSpeed = 1000;
        weaponAccel = 0;
        weaponSizeH = 3f;
        weaponSizeW = 0.3f;
    }
    
    protected void Update () {
        checkBounds();
    }

    protected void kick()
    {
        Vector3 forceVector = weapon.transform.up * weaponSpeed;
        Debug.Log("projectile force vector: " + forceVector);
        weapon.rigidbody.AddForce(forceVector);
    }

    public void setTarget(GameObject t, string tag)
    {
    	target = t;
    	enemyTag = tag;
    	hasTarget = true;
    }

    public void setEnemyTag(string tag)
    {
        enemyTag = tag;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(weaponDamage);
        }
        else if(other.gameObject.tag == "Asteroid")
        {
            //Vector3 point_of_contact = (other.contacts[0]).point;
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

    protected void checkBounds()
    {
        float fun = Math.Abs(Vector3.Distance(initialPosition, weapon.transform.position));
        //Debug.Log("distance: " + fun); 
        if(fun > weaponRange)
        {
            Destroy(weapon);
        }
        /*
        float x_bounds = GameCamera.x_bounds;
        float y_bounds = GameCamera.y_bounds;

        if(Math.Abs(weapon.transform.position.x) > x_bounds || Math.Abs(weapon.transform.position.y) > y_bounds)
        {
            Destroy(weapon);
        }*/
    }
}
