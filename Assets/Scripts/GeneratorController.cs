using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class GeneratorController : MonoBehaviour {

	protected bool hacked = false;
	protected int timeToHack = 5000;
	protected tk2dSprite img;
	protected string baseSprite = "hackingStation";
	protected string hackedSprite = "hackedStation";

	Awake()
	{
		img.SetSprite(baseSprite);
	}

	Start()
	{
	}

	Update()
	{
	}

	public hackStation()
	{
		if( !hacked )
		{
			--timeToHack;
			if( timeToHack == 0 )
			{
				hacked = true;
				img.SetSprite(hackedSprite);
			}
		}
	}
}