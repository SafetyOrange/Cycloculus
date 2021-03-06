﻿using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	Vector3 fwd;
	public float splode;
	public float rads;
	bool fire = false;

	// Use this for initialization
	void Start () {

		fwd = transform.forward;
	
	}
	
	// Update is called once per frame
	void Update () {

//		if (Physics.Raycast(transform.position, fwd, 10))
//			print("There is something in front of the object!");
	
		if (Input.GetMouseButtonDown(0)) {
			fire = true;
		}
		if (Input.GetMouseButtonUp(0)) {
			fire = false;
			GetComponentInChildren<MeshRenderer>().enabled=false;
		}

		if (fire) {
			GetComponent<AudioSource>().audio.Play();
			GetComponentInChildren<MeshRenderer>().enabled=!GetComponentInChildren<MeshRenderer>().enabled;
		}
		
		RaycastHit smash;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, .5f));
			if (Physics.SphereCast(ray, 10, out smash) && fire){
				if (smash.rigidbody != null){
					Debug.DrawLine (ray.origin, smash.point);
					Collider[] colliders = Physics.OverlapSphere(smash.point, rads);
				foreach (Collider hit in colliders) {
					hit.rigidbody.AddExplosionForce(splode, smash.point, rads, 3);
					hit.gameObject.SendMessage("Die");
					if(!hit.rigidbody.isKinematic) hit.rigidbody.velocity = ray.direction * splode;

					// Play the explosion sound when the beam hits a cube.
					GameObject imHit = hit.transform.gameObject;
					if (imHit.tag == "Respawn") {
						AudioSource[] myAudios = imHit.GetComponents<AudioSource>();
						foreach (AudioSource thisAudio in myAudios) {
							if(thisAudio.clip.name == "Explosion8"){
								//if (!thisAudio.isPlaying) {
									thisAudio.Play();
								//}
							}
						}
					}
				}
			}
		}
	}
}
