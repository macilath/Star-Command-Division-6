using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public List<GameObject> PlayerShips = new List<GameObject>();
    public List<GameObject> EnemyShips = new List<GameObject>();
	// Use this for initialization
    void Start()
    {
        AddShips();
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
	
	void AddShips() {
		if(Application.loadedLevelName == "Level1") {
			// Add Ship object to selectable list
            GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerShip");
            for (int i = 0; i < players.Length; i++)
            {
                PlayerShips.Add(players[i]);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShip");
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyShips.Add(players[i]);
            }
            //Selectable.Add(PlayerShip); 
			
		}
	}
	
}
