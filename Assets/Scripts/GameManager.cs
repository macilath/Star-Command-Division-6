using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AddSelectables();		
	}
	
	void Update () {

	}
	
	public List<GameObject> Selectable = new List<GameObject>(); 
	
	void AddSelectables() {
		if(Application.loadedLevelName == "Scene1") {
			// Add Ship object to selectable list
			GameObject PlayerShip = GameObject.Find("Ship");
			Selectable.Add(PlayerShip); 
			print("Added " + PlayerShip);
		}
	}
	
    /* For Unit Selection:
     * 
     * The other (and probably better) way is to keep a list of all selectable units (in some kind of gamemanager) and just transform the positions of all units into screenspace. 
     * There you can simply use Rect.Contains to test if a unit is inside a rectangle.
     * (http://answers.unity3d.com/questions/287406/rts-rectangle-selection-system.html)
     * So here in GameManager we're going to keep an array (or similar structure) of GameObjects that we want to be selectable 
     * 
     */

}
