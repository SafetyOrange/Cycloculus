using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	AudioSource[] myAudios;
	Vector3 lastPos;
	bool die = false;
	float dieStart = 0;
	float i = 0;

	// Use this for initialization
	void Start () {
	
		myAudios = GetComponents<AudioSource>();
		lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
		lastPos = transform.position;

		if(die){
			if(Time.time < dieStart + 2){
				i += .01f;
				renderer.material.SetFloat("_Cutoff", i);
			} else Destroy(this.gameObject);
		}
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

	void Die () {

		die = true;
		dieStart = Time.time;

	}
}
