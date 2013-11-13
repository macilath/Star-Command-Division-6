using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public abstract class UnitController : MonoBehaviour {

    protected GameObject thisShip;
    protected GameObject Electric;
    protected GameObject ElectricEffect;
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
    protected Stopwatch stunTimer = new Stopwatch();
    protected int stunDuration;
    protected bool isActive = true;

    protected bool shipCanFire()
    {
        if(stopwatch.ElapsedMilliseconds == 0 || stopwatch.ElapsedMilliseconds >= fireInterval)
        {
            stopwatch.Reset();
            return true;
        }
        return false;
    }

    protected bool shipIsActive()
    {
        return isActive;
    }

    public void deactivate(int time)
    {
        isActive = false;
        stunTimer.Reset();
        stunTimer.Start();
        stunDuration = time;
        Vector3 effectPos = thisShip.transform.position;
        effectPos.z -= 0.5f;
        ElectricEffect = Instantiate(Electric, effectPos, thisShip.transform.rotation) as GameObject;
        ElectricEffect.transform.parent = thisShip.transform;
    }

    public virtual void checkStun()
    {
        if(stunTimer.ElapsedMilliseconds >= stunDuration)
        {
            stunTimer.Stop();
            isActive = true;
            if(ElectricEffect.activeInHierarchy)
            {
                Destroy(ElectricEffect);
            }
        }
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
        float angleDir = AngleDir(this.transform.up, toTarget, Vector3.forward);
        float targetAngle = Vector3.Angle(Vector3.up, toTarget) * angleDir;
        if (targetAngle < 0)
        {
            targetAngle += 360;
        }
        float rotationAngle = targetAngle - shipAngle;
        //Debug.Log("Rotate from " + shipAngle + " to " + targetAngle);
        //Debug.Log(rotationAngle + " degrees");
        //this.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        //facingTarget = true;
        //TODO: make rotation fluid instead of instant

        if(Math.Abs(rotationAngle) < 5)
        {
            this.rigidbody.angularVelocity = Vector3.zero;
            this.transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            facingTarget = true;
        }
        else if(this.rigidbody.angularVelocity.z < shipRotSpeed)
        {
            Vector3 rotate = new Vector3(0, 0, 5 * angleDir);
            this.rigidbody.AddTorque(rotate);
        }
    }

    protected void move(Vector3 shipPosition)
    {
        // Move ship
        Vector3 forceVector = (targetDest - shipPosition);
        forceVector.Normalize();
        Vector3 shipVelocity = this.rigidbody.velocity;

        Rect boundingRect = new Rect(shipPosition.x - (shipSizeW/2), shipPosition.y - (shipSizeH/2), shipSizeW, shipSizeH);
        //Debug.Log(shipPosition - targetDest);
        if (hasTarget && boundingRect.Contains(targetDest))
        {
            //thisShip.rigidbody.AddRelativeForce(-shipVelocity * thisShip.rigidbody.mass);
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
            UnityEngine.Debug.Log("Destination Reached.");
            hasTarget = false;
            return;
        }

        //TODO: change this to compare vectors using cosine to ensure ship is always trying to move to targetDest
        if (hasTarget)
        {
            if (shipVelocity.sqrMagnitude < shipSpeed)
            {
                forceVector = shipVelocity + (forceVector * shipAccel);
            }
            else
            {
                forceVector = new Vector3(0, 0, 0);
            }
        }
        /*else //TODO: use this idea to have ship slow down at destination instead of just stop instantly
        {
            if (shipVelocity.sqrMagnitude > 0)
            {
                forceVector = new Vector3(0, -1, 0);
                forceVector *= shipAccel;
                thisShip.rigidbody.AddRelativeForce(forceVector);
                return;
            }
        }*/

        this.rigidbody.AddForce(forceVector);
    }

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
