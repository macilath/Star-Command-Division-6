using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class GeneratorController : MonoBehaviour {

	protected bool hacked = false;
	protected int timeToHack = 5000;
    private Stopwatch hackWatch = new Stopwatch();
	protected tk2dSprite img;
	protected string baseSprite = "HackingStation1";
	protected string hackedSprite = "HackingStation2";
	protected static GameManager manager;

	void Awake()
	{
        img = this.GetComponent<tk2dSprite>();

        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();
	}

	void Start()
	{
	}

	void Update()
	{
		if( ! hacked )
		{
			checkHack();
		}
	}

	public void checkHack()
	{
        UnityEngine.Debug.Log(hackWatch.ElapsedMilliseconds + " ms");
		if( hackWatch.ElapsedMilliseconds >= timeToHack)
		{
			hacked = true;
			++manager.hackedStations;
            UnityEngine.Debug.Log("Setting sprite");
			img.SetSprite(hackedSprite);
		}
	}

	public void startHack()
	{
		if( ! hacked )
		{
	        hackWatch.Reset();
	        hackWatch.Start();
    	}
	}

	public void stopHack()
	{
		if( ! hacked )
		{
			hackWatch.Reset();
		}
		else
		{
			hackWatch.Stop();
		}
	}
}