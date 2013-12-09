using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Font digiFont;
    public Font cyberFont;
    public static GameManager manager;
    private static HackerController hacker;
	GUIStyle warningClock = new GUIStyle();
	GUIStyle dangerClock = new GUIStyle();
	GUIStyle warningStyle = new GUIStyle();
	GUIStyle dangerStyle = new GUIStyle();
    GUIStyle buttonStyle = new GUIStyle();
    private GUIContent content = new GUIContent();
    public int loadedLevel = 0;
    //public tk2dSprite healthBar; 
    public GUITexture healthBar;

    void Awake()
    {
        loadedLevel = GameObject.Find("SettingsManager").GetComponent<SettingsManager>().lastLoadedLevel; 
        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();
        
        //digiFont = (Font)Resources.Load("Fonts/Digitalism", typeof(Font));
        digiFont = Resources.Load("digitalism") as Font;
        cyberFont = Resources.Load("cyberspace") as Font;
        buttonStyle.font = cyberFont;

        content.image = Resources.Load("GenericButton") as Texture2D;
        content.text = "";

        if(Application.loadedLevelName == "Level1")
        {
            warningClock.normal.textColor = Color.yellow;
            dangerClock.normal.textColor = Color.red;
            warningStyle.normal.textColor = Color.yellow;
            dangerStyle.normal.textColor = Color.red;

            warningClock.font = digiFont;
            dangerClock.font = digiFont;

            warningClock.fontSize = 20;
            dangerClock.fontSize = 20;
            warningStyle.fontSize = 15;
            dangerStyle.fontSize = 15;
        }
        if(Application.loadedLevelName == "Level2")
        {
            warningStyle.font = digiFont;
            warningStyle.normal.textColor = Color.white;
            warningStyle.fontSize = 20;
        }
    }

    void OnGUI()
    {
        GUI.skin.button.normal.background = (Texture2D)content.image;
        GUI.skin.button.hover.background = (Texture2D)content.image;
        GUI.skin.button.active.background = (Texture2D)content.image;
        // Title Screen
        if (Application.loadedLevelName == "Title")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 175, Screen.height / 2 + 300, 100, 110), "Easy"))
            {
                print("Play easy");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 1;
                Application.LoadLevel("Intro");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 300, 110, 110), "Medium"))
            {
                print("Play");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 2;
                Application.LoadLevel("Intro");
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 95, Screen.height / 2 + 300, 100, 110), "Hard"))
            {
                print("Play hard");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 3;
                Application.LoadLevel("Intro");
            }
        }

        else if (Application.loadedLevelName == "HowTo")
        {
            if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 - 100, 110, 110), "PLAY"))
            {
                //GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 2;
                Application.LoadLevel("Intro");
            }
        }

        // Intro Cut Scene Screen
        else if (Application.loadedLevelName == "Intro")
        {
            // Intro UI Elements, Story, etc.
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 110, 110), "PLAY"))
            {
                //GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 2;
                Application.LoadLevel("Level1");
            }
        }



        // Level 1
        else if (Application.loadedLevelName == "Level1")
        {
            // GUI elements for first level

            if (manager.alertStopwatch.ElapsedMilliseconds != 0)
            {
                string timeRemaining = string.Format("{0}", (manager.alertWindow - manager.alertStopwatch.ElapsedMilliseconds) / 1000);
                if ((manager.alertWindow - manager.alertStopwatch.ElapsedMilliseconds) / 1000 <= 30)
                {
                    GUI.Label(new Rect(40, Screen.height - 70, 200, 40), "You've been spotted!\n Time Remaining:", dangerStyle);
                    GUI.Label(new Rect(100, Screen.height - 30, 200, 40), timeRemaining, dangerClock);
                    string shipsRemaining = string.Format("{0}", manager.PlayerShips.Count);
                    GUI.Label(new Rect(Screen.width - 125, Screen.height - 60, 200, 40), "Ships Remaining: ", dangerStyle);
                    GUI.Label(new Rect(Screen.width - 75, Screen.height - 30, 200, 40), shipsRemaining, dangerClock);
                }
                else
                {
                    GUI.Label(new Rect(40, Screen.height - 70, 200, 40), "You've been spotted!\n Time Remaining:", warningStyle);
                    GUI.Label(new Rect(100, Screen.height - 30, 200, 40), timeRemaining, warningClock);
                    string shipsRemaining = string.Format("{0}", manager.PlayerShips.Count);
                    GUI.Label(new Rect(Screen.width - 125, Screen.height - 60, 200, 40), "Ships Remaining: ", warningStyle);
                    GUI.Label(new Rect(Screen.width - 75, Screen.height - 30, 200, 40), shipsRemaining, warningClock);
                }
                //string shipsRemaining = string.Format("Ships Remaining: {0}", manager.PlayerShips.Count);
                //GUI.Label(new Rect(Screen.width - 100, Screen.height - 20, 200, 40), "Ships Remaining: " + shipsRemaining);
            }
        }

        // Level 2
        else if (Application.loadedLevelName == "Level2")
        {

            GameObject health = GameObject.Find("HackerHealthBar");
            //healthBar = health.GetComponent<tk2dSprite>();
            healthBar = health.GetComponent<GUITexture>();
            //healthBar.pixelInset = new Rect(325, -375, 200, 200);
            healthBar.pixelInset = new Rect( (Screen.width / 2) - 200, 310 - (Screen.height), 200, 200);
            GUI.Label(new Rect(Screen.width - 200, Screen.height - 60, 200, 40), "Hacker Health", warningStyle);

            GameObject hacky = GameObject.Find("HackerShip");
            int hackerHealth = hacky.GetComponent<HackerController>().shipHealth;
            //string hackerHealth = string.Format("Hacker Health: {0}", hacky.GetComponent<HackerController>().shipHealth);
            //GUI.Label(new Rect(Screen.width - 150, Screen.height - 20, 200, 40), hackerHealth);
            Vector3 camPosition = manager.camera.transform.position;
            /*Vector3 healthPosition = camPosition;
            healthPosition.x = camPosition.x + 20;
            healthPosition.y = camPosition.y - 10;
            healthPosition.z = 0;J*/
            //healthBar.transform.position = healthPosition;            

            if (hackerHealth <= 50)
            {
                //healthBar.SetSprite("HealthBar_3");
                healthBar.texture = Resources.Load("HealthBar_3") as Texture2D;
            }
            else if (hackerHealth <= 100)
            {
                //healthBar.SetSprite("HealthBar_2");
                healthBar.texture = Resources.Load("HealthBar_2") as Texture2D;
            }
            else
            {
                //healthBar.SetSprite("HealthBar_1");
                healthBar.texture = Resources.Load("HealthBar_1") as Texture2D;
            }

        }

        // Level 3
        else if (Application.loadedLevelName == "Level3")
        {
            // GUI elements for last level
        }

        // Lose
        else if (Application.loadedLevelName == "Loss")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 60, 100, 110), "Try Again"))
            {
                print("Level: " + loadedLevel);
                switch (loadedLevel)
                {
                    case 0: break;
                    case 1:
                        print("Reload level 1");
                        Application.LoadLevel("Level1");
                        break;
                    case 2:
                        print("Reload level 2");
                        Application.LoadLevel("Level2");
                        break;
                    case 3:
                        print("Reload level 3");
                        GameManager.playerAlive = true;
                        GameManager.hostageAlive = true;
                        GameManager.hostageSafe = false;
                        Application.LoadLevel("Level3");
                        break;
                }
            }
        }

        // Transitional scenes - If scene is loaded then we won the previous level
        else if (Application.loadedLevelName == "1to2")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 110), "PLAY"))
            {
                print("Play");
                Application.LoadLevel("Level2");
            }
        }
        else if (Application.loadedLevelName == "2to3")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 110), "PLAY"))
            {
                print("Play");
                GameManager.playerAlive = true;
                GameManager.hostageAlive = true;
                GameManager.hostageSafe = false;

                Application.LoadLevel("Level3");
            }
        }
    }



}
