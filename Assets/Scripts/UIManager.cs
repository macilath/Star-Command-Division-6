using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    GUIStyle myStyle = new GUIStyle();
    Font myFont;
    public static GameManager manager; 

    void OnGUI()
    {
        GameObject camera = GameObject.Find("Main Camera");
        manager = camera.GetComponent<GameManager>();

        // Title Screen
        if (Application.loadedLevelName == "Title")
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 40), "PLAY"))
            {
                print("Play");
                Application.LoadLevel("Level1");
            }
        }

        // Level 1
        else if (Application.loadedLevelName == "Level1")
        {

            // GUI elements for first level
            if (manager.alertStopwatch.ElapsedMilliseconds != 0)
            {
                string timeRemaining = string.Format("Death In: {0} Seconds", (manager.alertWindow - manager.alertStopwatch.ElapsedMilliseconds)/1000);
                GUI.Label(new Rect(10, 10, 200, 40), "You've been spotted!\n" + timeRemaining);
            }
        }

        // Level 2
        else if (Application.loadedLevelName == "Level2")
        {
            // GUI elements for second level
        }

        // Level 3
        else if (Application.loadedLevelName == "Level3")
        {
            // GUI elements for last level
        }

        // Lose 
        else if (Application.loadedLevelName == "L1Loss")
        {
            // GUI for losing in level 1
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 60, 100, 40), "Try Again"))
            {
                print("Replay from L1Loss");
                Application.LoadLevel("Level1");
            }
        }

        else if (Application.loadedLevelName == "L2Loss")
        {
            // GUI for losing in level 2
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 60, 100, 40), "Try Again"))
            {
                print("Replay from L2Loss");
                Application.LoadLevel("Level2");
            }
        }

        else if (Application.loadedLevelName == "L3Loss")
        {
            // GUI for losing in the last level
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 60, 100, 40), "Try Again"))
            {
                print("Replay from L3Loss");
                Application.LoadLevel("Level3");
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
