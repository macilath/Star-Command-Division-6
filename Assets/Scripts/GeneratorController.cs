using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class GeneratorController : MonoBehaviour {

	protected bool hacked = false;
	public int timeToHack = 5000;
    private Stopwatch hackWatch = new Stopwatch();
	protected tk2dSpriteAnimator img;
	protected string baseSprite = "HackingStation";
	protected string hackedSprite = "HackedStation";
	protected static GameManager manager;

	void Awake()
	{
        img = this.GetComponent<tk2dSpriteAnimator>();

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
        //UnityEngine.Debug.Log(hackWatch.ElapsedMilliseconds + " ms");
		if( hackWatch.ElapsedMilliseconds >= timeToHack)
		{
			hacked = true;
			++manager.hackedStations;
            UnityEngine.Debug.Log("Setting sprite");
			img.Play(hackedSprite);
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