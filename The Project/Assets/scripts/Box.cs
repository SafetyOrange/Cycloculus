using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	AudioSource[] myAudios;
	Vector3 lastPos;
	bool die = false;
	bool scoredPoint = false;
	float dieStart = 0;
	float i = 0;
	public int health=20;
	GameObject controller;

	// Use this for initialization
	void Start () {
	
		myAudios = GetComponents<AudioSource>();
		lastPos = transform.position;
		controller = GameObject.Find("pf_controller");
	}
	
	// Update is called once per frame
	void Update () {
	
		lastPos = transform.position;

		if(die){

			if(gameObject.transform.childCount>0){
				foreach(Renderer j in GetComponentsInChildren<Renderer>()){
					if(Time.time < dieStart + 2){
						i += .0005f;
						renderer.material.SetFloat("_Cutoff", i);
						j.material.SetFloat("_Cutoff",i);
					}else Destroy(this.gameObject);
				}
			}
			else{
				if(Time.time < dieStart + 2){
					i += .01f;
					renderer.material.SetFloat("_Cutoff", i);
				}else Destroy(this.gameObject);
			}
				
			if (!scoredPoint) {
				controller.GetComponent<makeObjects>().score++;
				scoredPoint = true;
			}
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

	void OnTriggerEnter(Collider other) {
		// Reset the score if collides with the player.
		if (other.gameObject.tag == "MainCamera") {
			controller.GetComponent<makeObjects>().score = 0;
			//Debug.Log("lose!");

			GameObject player = other.gameObject;
			foreach (AudioSource thisAudio in player.GetComponent<BMABeam>().myAudios) {
				if(thisAudio.clip.name == "lose"){
					if (!thisAudio.isPlaying) {
						thisAudio.Play();
					}
				}
			}
		}
	}

	void Hurt() {
		health--;
	}

	void Die () {

		die = true;
		dieStart = Time.time;

	}
}
