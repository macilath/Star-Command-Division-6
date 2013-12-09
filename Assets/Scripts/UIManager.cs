using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Font digiFont;
    public static GameManager manager;
    private static HackerController hacker;
	GUIStyle warningClock = new GUIStyle();
	GUIStyle dangerClock = new GUIStyle();
	GUIStyle warningStyle = new GUIStyle();
	GUIStyle dangerStyle = new GUIStyle();
    public int loadedLevel = 0;

    void OnGUI()
    {
        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();
        //digiFont = (Font)Resources.Load("Fonts/Digitalism", typeof(Font));
        loadedLevel = GameObject.Find("SettingsManager").GetComponent<SettingsManager>().lastLoadedLevel; 

        // Title Screen
        if (Application.loadedLevelName == "Title")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 175, Screen.height / 2 - 100, 100, 40), "PLAY - Easy"))
            {
                print("Play easy");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 1;
                Application.LoadLevel("Intro");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 110, 40), "PLAY - Medium"))
            {
                print("Play");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 2;
                Application.LoadLevel("Intro");
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 95, Screen.height / 2 - 100, 100, 40), "PLAY - Hard"))
            {
                print("Play hard");
                GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 1;
                Application.LoadLevel("Intro");
            }
        }
        
        // Intro Screen
        else if (Application.loadedLevelName == "Intro")
        {
            // Intro UI Elements, Story, etc.
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 110, 40), "PLAY"))
            {
                //GameObject.Find("SettingsManager").GetComponent<SettingsManager>().difficultyLevel = 2;
                Application.LoadLevel("Level1");
            }
        }

        // Level 1
        else if (Application.loadedLevelName == "Level1")
        {
            warningClock.normal.textColor = Color.yellow;
            dangerClock.normal.textColor = Color.red;

            warningClock.font = digiFont;
            dangerClock.font = digiFont;
            warningClock.fontSize = 20;
            dangerClock.fontSize = 20;
            warningStyle.fontSize = 15;
            dangerStyle.fontSize = 15;
            warningStyle.normal.textColor = Color.yellow;
            dangerStyle.normal.textColor = Color.red;


            hacker = manager.GetComponent<HackerController>();
            // GUI elements for first level
            if (manager.alertStopwatch.ElapsedMilliseconds != 0)
            {
                string timeRemaining = string.Format("{0}", (manager.alertWindow - manager.alertStopwatch.ElapsedMilliseconds) / 1000);
                if ((manager.alertWindow - manager.alertStopwatch.ElapsedMilliseconds) / 1000 <= 30)
                {
                    //GUI.Label(new Rect(40, Screen.height - 40, 200, 40), "You've been spotted!\n" + timeRemaining, dangerStyle);
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
            hacker = manager.GetComponent<HackerController>();
            // GUI elements for second level
            string hackerHealth = string.Format("Hacker Health: {0}", hacker.shipHealth);
            GUI.Label(new Rect(Screen.width - 100, Screen.height - 20, 200, 40), hackerHealth);

        }

        // Level 3
        else if (Application.loadedLevelName == "Level3")
        {
            // GUI elements for last level
        }

        // Lose
        else if (Application.loadedLevelName == "Loss")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 60, 100, 40), "Try Again"))
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
                        Application.LoadLevel("AlternativeLevel4");
                        break;
                }
            }
        }

        // Transitional scenes - If scene is loaded then we won the previous level
        else if (Application.loadedLevelName == "1to2")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 40), "PLAY"))
            {
                print("Play");
                Application.LoadLevel("Level2");
            }
        }
        else if (Application.loadedLevelName == "2to3")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 40), "PLAY"))
            {
                print("Play");
                Application.LoadLevel("Level3");
            }
        }
    }



}
