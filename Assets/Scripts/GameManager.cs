using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public List<GameObject> PlayerShips = new List<GameObject>();
    public List<GameObject> EnemyShips = new List<GameObject>();
	// Use this for initialization
    void Start()
    {
        AddSelectables();
    }

    void Update()
    {
        if (PlayerShips.Count == 0 && Application.loadedLevelName == "Level1")
        {
            Application.LoadLevel("L1Loss");
        }
        if (PlayerShips.Count == 0 && Application.loadedLevelName == "Level2") //or we lose special ship
        {
            //Application.LoadLevel("L2Loss");
        }
    }
	
	void AddSelectables() {
		if(Application.loadedLevelName == "Level1") {
			// Add Ship object to selectable list
            GameObject[] PlayerShip = GameObject.FindGameObjectsWithTag("PlayerShip");
            for (int i = 0; i < PlayerShip.Length; i++)
            {
                PlayerShips.Add(PlayerShip[i]);
            }

            //Selectable.Add(PlayerShip); 
			print("Added " + PlayerShip);
		}
	}
	
}
