using UnityEngine;
using System.Collections;
using System;

public abstract class UnitController : MonoBehaviour {

    public GameObject playerShip;
    protected bool isSelected;
    public static GameManager manager; 
    public int shipSpeed;
    public int shipAccel;
    public float shipSizeH;
    public float shipSizeW;
    public float shipRotSpeed;
    public Vector3 targetDest;
    protected bool hasTarget;
    protected bool facingTarget;
    protected bool targetIsEnemy;

    public abstract void getShipSelected(Vector3 shipPosition);

    public abstract void setTarget();

    public abstract float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up);

    public abstract void rotate(Vector3 shipPosition);

    public abstract void move(Vector3 shipPosition);
}
