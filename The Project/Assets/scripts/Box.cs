using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	AudioSource[] myAudios;
	Vector3 lastPos;

	// Use this for initialization
	void Start () {
	
		myAudios = GetComponents<AudioSource>();
		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		lastPos = transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		if (transform.position != lastPos) {
			foreach (AudioSource thisAudio in myAudios) {
				if(thisAudio.clip.name == "hit"){
					if (!thisAudio.isPlaying) {
						thisAudio.Play();
					}
				}
			}
		}
	}
}
