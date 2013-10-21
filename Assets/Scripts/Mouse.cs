using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mouse : MonoBehaviour {
	
	public static List<GameObject> UnitList = new List<GameObject>();
	public Texture2D selectionHighlight;
	public static Rect selection = new Rect(0,0,0,0);
	private Vector3 mouseDownPoint = -Vector3.one;
	
	// Update is called once per frame
	void Update () {
		CheckCamera();
	}
	
	private void CheckCamera()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseDownPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0))
		{	
			mouseDownPoint = -Vector3.one;
		}
		
		if (Input.GetMouseButton(0))
		{
			selection = new Rect(mouseDownPoint.x, mouseDownPoint.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mouseDownPoint.x,
								Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mouseDownPoint.y);
			
			if (selection.width < 0)
			{
				selection.x += selection.width;
				selection.width = - selection.width;
			}
			
			if (selection.height < 0)
			{
				selection.y += selection.height;
				selection.height = - selection.height;
			}
		}
	}
	
	private void OnGUI()
	{
	 	if (mouseDownPoint != - Vector3.one)
		{
			GUI.color = new Color(1, 1, 1, 0.5f);
			GUI.DrawTexture(selection, selectionHighlight);
		}
	}
	
	/*public static float InverseMouseY(float y)
	{
		return Screen.height - y;	
	}*/
}
