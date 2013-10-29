using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    GUIStyle myStyle = new GUIStyle();
    Font myFont;

    void OnGUI()
    {
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
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 - 100, 100, 40), "Try Again"))
            {
                print("Replay from L1Loss");
                Application.LoadLevel("Level1");
            }
        }

        else if (Application.loadedLevelName == "L2Loss")
        {
            // GUI for losing in level 2
        }

        // Victory
        else if (Application.loadedLevelName == "L1Victory")
        {
            // GUI for passing level 1 (continuing story)
        }

        else if (Application.loadedLevelName == "L2Victory")
        {
            // GUI for passing level 2 (continuing story)
        }
    }



}
