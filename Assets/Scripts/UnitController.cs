using UnityEngine;
using System.Collections;
using System;

public abstract class UnitController : MonoBehaviour {

    protected GameObject thisShip;
    protected bool isSelected;
    public static GameManager manager; 
    protected int shipSpeed;
    protected int shipAccel;
    protected float shipSizeH;
    protected float shipSizeW;
    protected float shipRotSpeed;
    protected int shipHealth;
    protected int maxHealth;
    protected Vector3 targetDest;
    protected bool hasTarget;
    protected bool facingTarget;
    protected bool targetIsEnemy;
    protected int fireInterval = 1000;
    protected Stopwatch stopwatch = new Stopwatch();

    protected bool shipCanFire()
    {
        if(stopwatch.ElapsedMilliseconds == 0 || stopwatch.ElapsedMilliseconds >= fireInterval)
        {
            stopwatch.Reset();
            return true;
        }
        return false;
    }

    protected abstract void getShipSelected(Vector3 shipPosition);

    public abstract void setTarget();

    public abstract void takeDamage(int damage);

    public void Awake()
    {
        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();
    }
    
    protected float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        
        if (dir > 0f) {
            return 1f;
        } else if (dir < 0f) {
            return -1f;
        } else {
            return 0f;
        }
    }

    protected void rotate(Vector3 shipPosition)
    {
        Vector3 toTarget = targetDest - shipPosition;
        float shipAngle = this.transform.rotation.eulerAngles.z;
        float targetAngle = Vector3.Angle(Vector3.up, toTarget) * AngleDir(Vector3.up, toTarget, Vector3.forward);
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        float rotationAngle = targetAngle - shipAngle;
        //Debug.Log("Rotate from " + shipAngle + " to " + targetAngle);
        //Debug.Log(rotationAngle + " degrees");
        this.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        facingTarget = true;
        //TODO: make rotation fluid instead of instant
    }

    protected abstract void move(Vector3 shipPosition);

    protected void checkHealth()
    {
        if(shipHealth <= 0)
        {
            GameObject Explosion = (GameObject)Resources.Load("ShipExplode1");
            Instantiate(Explosion, thisShip.transform.position, Quaternion.identity);
            if (thisShip.tag == "EnemyShip")
            {
                manager.EnemyShips.Remove(thisShip);
            }
            else if (thisShip.tag == "PlayerShip")
            {
                manager.PlayerShips.Remove(thisShip);
            }
            Destroy(thisShip);
        }
    }

    protected abstract void fireWeapons();
}
