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

	Awake()
	{
		img.SetSprite(baseSprite);
        manager = camera.GetComponent<GameManager>();
	}

	Start()
	{
	}

	Update()
	{
		if( ! hacked )
		{
			checkHack();
		}
	}

	public checkHack()
	{
		if( hackWatch.ElapsedMilliseconds >= timeToHack)
		{
			hacked = true;
			++manager.hackedStations;
			img.SetSprite(hackedSprite);
		}
	}

	public startHack()
	{
		if( ! hacked )
		{
	        hackWatch.Reset();
	        hackWatch.Start();
    	}
	}

	public stopHack()
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