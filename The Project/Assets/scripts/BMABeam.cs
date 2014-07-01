using UnityEngine;
using System.Collections;

public class BMABeam : MonoBehaviour {
	
	Vector3 fwd;
	public float splode;
	public float rads;
	public AudioSource[] myAudios;
	public ParticleSystem beamBlast;

	bool arm, fire = false;
	float chargeTimer, fireTimer = -1;
	float shotVal = -1;
	int chargeLimit = 3000; 			//This is in milliseconds
	int perfectCharge = 2500;
	int perfectBuffer = 200;
	float fireDur = 1500;


	// Use this for initialization
	void Start () {
		
		fwd = transform.forward;
		myAudios = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		MeasureShot();
		ComputeShot();
		FireShot();
		FirePhysics();

	}

	void MeasureShot(){
		
		if(Input.GetMouseButtonDown(0) && !arm && !fire){
			arm = true;
			chargeTimer = Time.time * 1000;
			Debug.Log("Charging");
		}
		
		if(Input.GetMouseButtonUp(0) && arm){
			shotVal = Time.time * 1000 - chargeTimer;
			arm = false;
		}
		else if(Time.time*1000 >= chargeTimer+chargeLimit && arm){
			shotVal = chargeTimer + chargeLimit;
			arm=false;
			Debug.Log("Overload");
		}
	}
	
	void ComputeShot(){
		
		if(shotVal != -1 && !arm){
			if(shotVal<perfectCharge-perfectBuffer){		//Undershot
				Debug.Log ("Weak");
				map(splode,0,100,shotVal,2500);
			}
			else if(shotVal>perfectCharge+perfectBuffer){	//Too much
				Debug.Log ("Too Much");
				map(rads,0,5,shotVal,3000);
			}
			else{											//Perfect
				Debug.Log ("Perfect");
			}

			fire=true;
			fireTimer = Time.time * 1000;
			ResetFiring();
		}
	}
	
	void FireShot(){
		
		if(fire){

			beamBlast.particleSystem.enableEmission = true;
			foreach (AudioSource thisAudio in myAudios) {
				if(thisAudio.clip.name == "Laser_Shoot2"){
					thisAudio.Play();
				}
			}

			GetComponentInChildren<Light>().enabled=true;
			MeshRenderer[] beamBoxes = GetComponentsInChildren<MeshRenderer>(); 
			foreach (MeshRenderer box in beamBoxes) {
				box.enabled = true;
			}

			if(Time.time * 1000 - fireTimer >= fireDur){
				fire=false;
				ResetFiring();
			}
		}
	}

	void FirePhysics(){

		RaycastHit smash;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.4325f, 0.5f, 0.5f));
		if (Physics.SphereCast(ray, 0.5f, out smash) && fire){
			if (smash.rigidbody != null){
				Debug.DrawLine (ray.origin, smash.point);
				GameObject.Find("beamSplode").transform.position=smash.point; //THIS IS BROKEN OMG
				if(!GameObject.Find("beamSplode").particleSystem.isPlaying) GameObject.Find("beamSplode").particleSystem.Play();
				Collider[] colliders = Physics.OverlapSphere(smash.point, rads);
				foreach (Collider hit in colliders) {
					if (hit.rigidbody != null){// && hit.gameObject.tag == "Respawn") {
						hit.gameObject.SendMessage("Hurt");

						if(hit.gameObject.GetComponent<Box>().health <= 0){

							hit.rigidbody.AddExplosionForce(splode, smash.point, rads, 3);
							hit.gameObject.SendMessage("Die");
							if(!hit.rigidbody.isKinematic) hit.rigidbody.velocity = ray.direction * splode;
						}
							// Play the explosion sound when the beam hits a cube.
							GameObject imHit = hit.transform.gameObject;
							if (imHit.tag == "Respawn") {
								AudioSource[] myAudios = imHit.GetComponents<AudioSource>();
								foreach (AudioSource thisAudio in myAudios) {
									if(thisAudio.clip.name == "Explosion8"){
										thisAudio.Play();
								}
							}
						}
					}
				}
			}
		}
	}

	void ResetFiring(){

		chargeTimer = -1;		//Must be done when inactive
		shotVal = -1;

		if(GameObject.Find("beamSplode").particleSystem.isPlaying) GameObject.Find("beamSplode").particleSystem.Stop();
		beamBlast.particleSystem.enableEmission = false;
		MeshRenderer[] beamBoxes = GetComponentsInChildren<MeshRenderer>(); 
		foreach (MeshRenderer box in beamBoxes) {
			box.enabled = false;
		}

		splode=90;
		rads=1;
		
		GetComponentInChildren<MeshRenderer>().enabled=false;
		GetComponentInChildren<Light>().enabled=false;
	}

	float map(float s, float a1, float a2, float b1, float b2){
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
		}
}
