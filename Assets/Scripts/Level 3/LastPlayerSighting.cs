using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {

	public Vector3 position = new Vector3(1000f, 1000f, 1000f);
	public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
	public float musicFadeSpeed = 1f;
	
	public AudioSource panicAudio;
	
	void Awake ()
    {   
        // Setup the reference to the additonal audio source.
        panicAudio = this.transform.GetChild(0).GetComponent<AudioSource>();//transform.FindChild("secondaryMusic").audio;
    }
	
	void Update ()
    {
        MusicFading();
    }
    
    void MusicFading ()
    {
        // If the alarm is not being triggered...
        if(position != resetPosition)
        {
            // ... fade out the normal music...
            audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            
            // ... and fade in the panic music.
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 1f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {

            // Otherwise fade in the normal music and fade out the panic music.
            audio.volume = Mathf.Lerp(audio.volume, 1f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}
