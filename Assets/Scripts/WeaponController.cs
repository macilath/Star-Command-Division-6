using UnityEngine;
using System.Collections;
using System;

public class WeaponController : MonoBehaviour {

    private int weaponDamage;
    private int weaponRange;
    private int weaponSpeed;
    private int weaponAccel;
    private float weaponSizeH;
    private float weaponSizeW;
    private bool isCollided;
    private bool hasTarget;
    private string enemyTag;

    private Vector3 initialPosition;

    private GameObject weapon;
    private GameObject target;
    private GameObject parentShip;

    void Start () {
    	weaponDamage = 25;
	    weaponRange = 10;
	    weaponSpeed = 500;
	    weaponAccel = 0;
	    weaponSizeH = 3f;
	    weaponSizeW = 0.3f;
	    isCollided = false;
	    hasTarget = false;
	    weapon = this.gameObject;
        initialPosition = weapon.transform.position;
        kick();
        //for debugging, should be determined by firing ship
        //enemyTag = "EnemyShip";
    }
    
    void Update () {
    }

    private void kick()
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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent<UnitController>())).takeDamage(weaponDamage);
        }
        else if(other.gameObject.tag == "Asteroid")
        {
            Vector3 point_of_contact = (other.contacts[0]).point;
            other.rigidbody.AddForceAtPosition((this.rigidbody.velocity) * 5, point_of_contact);
        }
        Destroy(weapon);
    }

    private void checkBounds()
    {
        if(Vector3.Distance(initialPosition, weapon.transform.position) > weaponRange)
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
