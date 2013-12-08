using UnityEngine;
using System.Collections;

public class Level3Camera : MonoBehaviour {

    public Camera camera;
	public GameObject player;
	
	public void Awake()
    {
		camera = GetComponent<Camera>();
        player = GameObject.Find("PlayerAnchor");
    }
	
	void Start()
	{
		
	}
	
	void Update()
	{
		snapToPlayer();
	}
	
	public void snapToPlayer()
	{
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            playerPos.y = camera.transform.position.y;
            camera.transform.position = playerPos;
        }
	}
}
