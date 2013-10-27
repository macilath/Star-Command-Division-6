using UnityEngine;
using System.Collections;
using System;

public abstract class UnitController : MonoBehaviour {

    protected GameObject playerShip;
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

    protected abstract void getShipSelected(Vector3 shipPosition);

    public abstract void setTarget();

    public abstract void takeDamage(int damage);

    protected abstract float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up);

    protected abstract void rotate(Vector3 shipPosition);

    protected abstract void move(Vector3 shipPosition);

    protected abstract void checkHealth();

    protected abstract void fireWeapons();
}
