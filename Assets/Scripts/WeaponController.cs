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

    private GameObject weapon;
    private GameObject target;
    private GameObject parentShip;

    void Start () {
    	weaponDamage = 5;
	    weaponRange = 10;
	    weaponSpeed = 50;
	    weaponAccel = 0;
	    weaponSizeH = 3f;
	    weaponSizeW = 0.3f;
	    isCollided = false;
	    hasTarget = false;
	    weapon = this.gameObject;

        this.rigidbody.AddRelativeForce((new Vector3(0, -1, 0)) * weaponSpeed);
    }
    
    void Update () {
    }

    public void setTarget(GameObject t, string tag)
    {
    	target = t;
    	enemyTag = tag;
    	hasTarget = true;
    }

    public void setParent(GameObject obj)
    {
        parentShip = obj;
        Debug.Log("Weapon Name: " + weapon.name);
        Debug.Log("Parent Name: " + parentShip.name);
        Physics.IgnoreCollision(weapon.collider, parentShip.collider);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == enemyTag)
        {
            ((UnitController)((other.gameObject).GetComponent("UnitController"))).takeDamage(weaponDamage);
        }
        else if(other.gameObject.tag == "Asteroid")
        {
            Vector3 point_of_contact = (other.contacts[0]).point;
            other.rigidbody.AddForceAtPosition((this.rigidbody.velocity) * 5, point_of_contact);
        }
        Destroy(this.gameObject);
    }

    private void checkBounds()
    {
        float x_bounds = GameCamera.x_bounds;
        float y_bounds = GameCamera.y_bounds;

        if(Math.Abs(weapon.transform.position.x) > x_bounds || Math.Abs(weapon.transform.position.y) > y_bounds)
        {
            Destroy(this.gameObject);
        }
    }
}
