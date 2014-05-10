using UnityEngine;
using System.Collections;

public class makeObjects : MonoBehaviour {

	GameObject[] targetList;
	public GameObject theCube, mann1, mann2, mann3, mann4;

	// Use this for initialization
	void Start () {
		InvokeRepeating("makeStuff", 2, 2);
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void makeStuff() {
		targetList = GameObject.FindGameObjectsWithTag("Respawn");
		//Debug.Log(targetList.Length);
		if (targetList.Length < 9) {
			GameObject tmp = theCube;
			float die = Random.value;
			float numSides = 5f;
			if (die < 1f / numSides) {
				tmp = theCube;
			} else if (die >= 1f / numSides && die < (1f / numSides) * 2f) {
				tmp = mann1;
			} else if (die >= (1f / numSides) * 2f && die < (1f / numSides) * 3f) {
				tmp= mann2;
			} else if (die >= (1f / numSides) * 3f && die < (1f / numSides) * 4f) {
				tmp = mann3;
			} else if (die >= (1f / numSides) * 4f) {
				tmp = mann4;
			}
			GameObject newThang = Instantiate(tmp, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			Vector3 flyToPlayer = Camera.main.transform.position - newThang.transform.position; // Calculate the vector towards the player.
			//Debug.Log(flyToPlayer);
			//newThang.rigidbody.AddForce(new Vector3(0, 1000f, 0));
			//float mult = 100f;
			//Debug.Log(flyToPlayer.magnitude);
			//newThang.rigidbody.AddForce(flyToPlayer * flyToPlayer.magnitude);
			//newThang.rigidbody.AddForce(new Vector3(flyToPlayer.x, 100f, flyToPlayer.z));
			newThang.rigidbody.velocity = BallisticVel(newThang.transform);
			Debug.Log(newThang.rigidbody.velocity);
		}
	}

	Vector3 BallisticVel(Transform projectile) {
		Vector3 dir = Camera.main.transform.position - projectile.position; // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		dir.y = dist;  // set elevation to 45 degrees
		dist += h;  // correct for different heights
		float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
		return vel * dir.normalized;  // returns Vector3 velocity
	}
}
