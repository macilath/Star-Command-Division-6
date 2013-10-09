using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}

    /* For Unit Selection:
     * 
     * The other (and probably better) way is to keep a list of all selectable units (in some kind of gamemanager) and just transform the positions of all units into screenspace. 
     * There you can simply use Rect.Contains to test if a unit is inside a rectangle.
     * (http://docs.unity3d.com/Documentation/ScriptReference/Camera.WorldToScreenPoint.html)
     * (http://docs.unity3d.com/Documentation/ScriptReference/Rect.Contains.html)
     * (http://answers.unity3d.com/questions/287406/rts-rectangle-selection-system.html)
     * So here in GameManager we're going to keep an array (or similar structure) of GameObjects that we want to be selectable 
     * In the player's unit controller (which extends UnitController.cs) we can check on mouse click if the rect contains / collides with the game object
     * Then we wait on right click (orders) to assign the unit's destination, or if another left click is detected we deselect the unit
     * 
     */

}
