using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {

    public int lastLoadedLevel = 0;
    public int difficultyLevel = 1;

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
