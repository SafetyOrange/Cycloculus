using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	AudioSource[] myAudio;
	Vector3 lastPos;

	// Use this for initialization
	void Start () {
	
		myAudio = GetComponents<AudioSource>();
		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		lastPos = transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		if (transform.position != lastPos) {
			foreach (AudioSource clip in myAudio) {
				if(clip.clip.name == "hit"){
					clip.Play();
				}
			}
		}
	}
}
