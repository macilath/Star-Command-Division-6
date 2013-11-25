using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mouse : MonoBehaviour
{

    public static List<GameObject> UnitList = new List<GameObject>();
    public Texture2D selectionHighlight;
    public static Rect selection = new Rect(0, 0, 0, 0);
    private Vector3 mouseDownPoint = -Vector3.one;
    private Vector3 mouseDown = -Vector3.one;
    private Vector3 worldMousePosition = -Vector3.one;
    public GameObject selectionBox;
    public GameManager manager;

    void Awake()
    {
        manager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        selectionBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        selectionBox.AddComponent<SelectionBox>();
        selectionBox.AddComponent<Rigidbody>();
        selectionBox.transform.position = Vector3.zero;
        Color c = selectionBox.renderer.material.color;
        c.a = 0.5f;
        Shader shade = Shader.Find("Transparent/Diffuse");
        selectionBox.renderer.material.shader = shade;
        selectionBox.renderer.material.color = c;
        selectionBox.GetComponent<MeshRenderer>().enabled = false;
        selectionBox.GetComponent<BoxCollider>().isTrigger = true;
        selectionBox.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCamera();
    }

    private void CheckCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPoint = Input.mousePosition;
            selectionBox.GetComponent<MeshRenderer>().enabled = true;
            selectionBox.GetComponent<BoxCollider>().enabled = true;
            foreach( GameObject obj in manager.PlayerShips )
            {
                obj.GetComponent<PlayerController>().getShipSelected(false);
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDownPoint = -Vector3.one;
            selectionBox.GetComponent<MeshRenderer>().enabled = false;
            selectionBox.GetComponent<BoxCollider>().enabled = false; 
        }

        if (Input.GetMouseButton(0))
        {
            
            mouseDown = camera.ScreenToWorldPoint(mouseDownPoint);
            worldMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            /*
            //selection = new Rect(mouseDownPoint.x, InverseMouseY(mouseDownPoint.y), Input.mousePosition.x - mouseDownPoint.x,
            //                    InverseMouseY(Input.mousePosition.y) - InverseMouseY(mouseDownPoint.y));
            selection = new Rect(mouseDown.x, mouseDown.y, worldMousePosition.x - mouseDown.x, worldMousePosition.y - mouseDown.y); */

            mouseDown.z = 0;
            selectionBox.transform.position = mouseDown;
            selectionBox.transform.localScale = worldMousePosition - mouseDown; 
            selectionBox.transform.Translate((worldMousePosition-mouseDown) / 2 , Space.World);
            Vector3 boxPos = selectionBox.transform.position;
            boxPos.z = 0;
            selectionBox.transform.position = boxPos;
            /*
            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }

            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            } */
        }
    }

    /*
    private void OnGUI()
    {
        //if (mouseDownPoint != -Vector3.one)
        if (mouseDown != -Vector3.one) 
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(selection, selectionHighlight);
        }
    }
     * */
    
    public static float InverseMouseY(float y)
    {
        return Screen.height - y;
    }
}
