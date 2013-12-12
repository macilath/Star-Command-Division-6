using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Lv3Player : HumanController {
	
	public bool hasKey1;
	private Vector3 lookRotationPoint;
	private Stopwatch shotTimer = new Stopwatch();
	private int fireInterval = 1000;
	
	void Awake()
	{
		hasKey1 = false;
	}
	
    void Update () 
    {
        base.Update();        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lookRotationPoint = hit.point - transform.position;
            transform.rotation = Quaternion.LookRotation(lookRotationPoint.normalized, transform.forward);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
		
		if (Input.GetMouseButtonDown(0))
        {
			// Not working yet, so its commented out :(
			if (canShoot())
	        {
	            fireWeapons();
	        }
		}
    }
	
	 protected bool canShoot()
    {
        if (this.transform.FindChild("HumanSprite").gameObject != null)
        {
            if (shotTimer.ElapsedMilliseconds == 0 || shotTimer.ElapsedMilliseconds >= fireInterval)
            {
                shotTimer.Reset();
                return true;
            }
        }
        return false;
    }
	

    protected void fireWeapons()
    {
        GameObject Projectile = (GameObject)Resources.Load("PersonalProjectile");
        Vector3 projectile_position = this.transform.FindChild("HumanSprite").position + (this.transform.FindChild("HumanSprite").up * 10);
        GameObject projObject = Instantiate(Projectile, projectile_position, this.transform.FindChild("HumanSprite").rotation) as GameObject;

        PersonalProjectile proj = projObject.GetComponent<PersonalProjectile>();
        proj.setEnemyTag("EnemyShip");

        shotTimer.Start();
    }

    public override void kill()
    {
        GameManager.playerAlive = false;
        Destroy(this.gameObject);
    }
}
