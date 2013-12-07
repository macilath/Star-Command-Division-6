using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public int lastLoadedLevel = 0; 

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
