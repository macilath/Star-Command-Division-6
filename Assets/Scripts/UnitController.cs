using UnityEngine;
using System.Collections;
using System;

public abstract UnitController{

    public GameObject playerShip;
    private bool isSelected;
    public static GameManager manager; 
    public int shipSpeed;
    public int shipAccel;
    public float shipSizeH;
    public float shipSizeW;
    public float shipRotSpeed;
    public Vector3 targetDest;
    private bool hasTarget;
    private bool facingTarget;
    private bool targetIsEnemy;

    void getShipSelected(Vector3 shipPosition);

    void setTarget();

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up);

    void rotate(Vector3 shipPosition);

    void move(Vector3 shipPosition);
}
