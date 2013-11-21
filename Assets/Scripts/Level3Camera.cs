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
	}

	void FixedUpdate()
	{
		snapToPlayer();
	}
	
	public void snapToPlayer()
	{
        Vector3 playerPos = player.transform.position;
		playerPos.y = camera.transform.position.y;
			
		Vector3 amountMoved = shipPos - camera.transform.position;
			
		camera.transform.position = playerPos;
	}
}
