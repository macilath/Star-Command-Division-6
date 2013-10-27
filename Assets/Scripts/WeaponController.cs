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

    private GameObject weapon;
    private GameObject target;

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
    }
    
    void Update () {
    	if(hasTarget)
    	{
	    	if(!isCollided)
	    	{
	    		checkCollision();
	    		move();
	    	}
	    	else
	    	{

	    	}
	    }
    }

    public void setTarget(GameObject t)
    {
    	target = t;
    	hasTarget = true;
    }

    private void checkCollision()
    {
    	Vector3 weaponPosition = weapon.transform.position;
    	Rect boundingRect = new Rect(weaponPosition.x - 8, weaponPosition.y - 75, 16, 150);
        if (boundingRect.Contains(target.transform.position))
        {
        	Debug.Log("Shot collided with target.");
        	isCollided = true;
        }
    }

    private void move()
    {

    }
}
