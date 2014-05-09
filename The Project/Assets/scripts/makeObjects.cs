using UnityEngine;
using System.Collections;

public class makeObjects : MonoBehaviour {

	GameObject[] targetList;
	public GameObject theCube, mann1, mann2, mann3, mann4;

	// Use this for initialization
	void Start () {
		InvokeRepeating("makeStuff", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void makeStuff() {
		targetList = GameObject.FindGameObjectsWithTag("Respawn");
		//Debug.Log(targetList.Length);
		if (targetList.Length < 100) {
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
			GameObject newThang = Instantiate(tmp, new Vector3(0, 10f, 0), Quaternion.identity) as GameObject;
		}
	}
}
