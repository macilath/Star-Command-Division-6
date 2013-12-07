using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class GameManager : MonoBehaviour {

    public List<GameObject> PlayerShips = new List<GameObject>();
    public List<bool> Survivors = new List<bool>();
    public List<GameObject> EnemyShips = new List<GameObject>();
    public System.Random rand = new System.Random();

    public Stopwatch alertStopwatch = new Stopwatch();
    public int alertWindow = 60000; //in ms, 1 min 
    public int hackedStations = 0;
    public bool hackerAlive = true;

    public int difficultyLevel = 2;
    //public int loadedLevel = 0; 

    void Start()
    {
        AddShips();
        //loadedLevel = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().lastLoadedLevel;
        if (Application.loadedLevelName == "Level1")
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().lastLoadedLevel = 1;
            print("Loaded level 1");
            int load = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().lastLoadedLevel;
            print(load);
        }
        if (Application.loadedLevelName == "Level2")
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().lastLoadedLevel = 2;
            int load = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().lastLoadedLevel;
            print(load);
        }
    }

    void Update()
    {
        if( Application.loadedLevelName == "Level1")
        {
            Level1Logic();
        }
        if( Application.loadedLevelName == "Level2")
        {
            Level2Logic();
        }
    }

    void Level1Logic()
    {
        if ( PlayerShips.Count == 0 )
        {
            Application.LoadLevel("Loss");
        }

        if ( AllShipsAccountedFor() )
        {
            UnityEngine.Debug.Log("Next Level");
            Application.LoadLevel("1to2");
        }

        if (alertStopwatch.ElapsedMilliseconds >= alertWindow || !hackerAlive) 
        {
            alertStopwatch.Reset();
            Application.LoadLevel("Loss");
        }
    }

    void Level2Logic()
    {
        if ( !hackerAlive )
        {
            Application.LoadLevel("Loss");
        }

        if( hackedStations == 4 )
        {
            GameObject shield = GameObject.Find("Shield");
            shield.GetComponent<MeshRenderer>().enabled = false;
            shield.GetComponent<MeshCollider>().isTrigger = true;
        }
    }

    bool AllShipsAccountedFor()
    {
        int numSurvivors = 0;
        foreach(bool saved in Survivors)
        {
            if(saved)
            {
                ++numSurvivors;
            }
        }
        //UnityEngine.Debug.Log("num survivors: " + numSurvivors);
        return (numSurvivors == PlayerShips.Count);
    }

    public void AddSurvivor(GameObject ship)
    {
        int index = PlayerShips.IndexOf(ship);
        Survivors[index] = true;
    }

    public void RemoveSurvivor(GameObject ship)
    {
        int index = PlayerShips.IndexOf(ship);
        Survivors[index] = false;
    }

    public void KillShip(GameObject ship)
    {
        int index = PlayerShips.IndexOf(ship);
        Survivors.RemoveAt(index);
        PlayerShips.RemoveAt(index);
    }
	
	void AddShips() {
		if(Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2") {
			// Add Ship object to selectable list
            GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerShip");
            for (int i = 0; i < players.Length; i++)
            {
                PlayerShips.Add(players[i]);
                Survivors.Add(false);
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShip");
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyShips.Add(enemies[i]);
            }
            GameCamera cam = GameObject.Find("Main Camera").GetComponent<GameCamera>();
            cam.shipIndex = PlayerShips.Count;
            cam.ShipIterate();
            //Selectable.Add(PlayerShip); 
			
		}
	}
}
