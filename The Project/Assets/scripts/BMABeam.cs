using UnityEngine;
using System.Collections;

public class BMABeam : MonoBehaviour {
	
	Vector3 fwd;
	public float splode;
	public float rads;
	bool fire = false;
	public AudioSource[] myAudios;
	public ParticleSystem beamBlast;


	// Use this for initialization
	void Start () {
		
		fwd = transform.forward;
		myAudios = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//		if (Physics.Raycast(transform.position, fwd, 10))
		//			print("There is something in front of the object!");

		
		if (Input.GetMouseButtonDown(0)) {
			fire = true;
			beamBlast.particleSystem.enableEmission = true;
		}
		if (Input.GetMouseButtonUp(0)) {
			fire = false;
			if(GameObject.Find("beamSplode").particleSystem.isPlaying) GameObject.Find("beamSplode").particleSystem.Stop();
			beamBlast.particleSystem.enableEmission = false;
			MeshRenderer[] beamBoxes = GetComponentsInChildren<MeshRenderer>(); 
			foreach (MeshRenderer box in beamBoxes) {
				box.enabled = false;
			}

			GetComponentInChildren<MeshRenderer>().enabled=false;
			GetComponentInChildren<Light>().enabled=false;
		}
		
		if (fire) {
			foreach (AudioSource thisAudio in myAudios) {
				if(thisAudio.clip.name == "Laser_Shoot2"){
					//if (!thisAudio.isPlaying) {
						thisAudio.Play();
					//}
				}
			}
			//GetComponentInChildren<MeshRenderer>().enabled=!GetComponentInChildren<MeshRenderer>().enabled;
			GetComponentInChildren<Light>().enabled=true;

			MeshRenderer[] beamBoxes = GetComponentsInChildren<MeshRenderer>(); 
			foreach (MeshRenderer box in beamBoxes) {
				box.enabled = true;
			}

		}
		
		RaycastHit smash;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, .5f));
		if (Physics.SphereCast(ray, 10, out smash) && fire){
			if (smash.rigidbody != null){
				Debug.DrawLine (ray.origin, smash.point);
				GameObject.Find("beamSplode").transform.position=smash.point; //THIS IS BROKEN OMG
				if(!GameObject.Find("beamSplode").particleSystem.isPlaying) GameObject.Find("beamSplode").particleSystem.Play();
				Collider[] colliders = Physics.OverlapSphere(smash.point, rads);
				foreach (Collider hit in colliders) {
					if (hit.rigidbody != null) {
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
}
