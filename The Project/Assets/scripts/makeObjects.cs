using UnityEngine;
using System.Collections;

public class makeObjects : MonoBehaviour {

	GameObject[] targetList;
	public GameObject theCube;

	// Use this for initialization
	void Start () {
		InvokeRepeating("makeCube", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void makeCube() {
		targetList = GameObject.FindGameObjectsWithTag("Respawn");
		//Debug.Log(targetList.Length);
		if (targetList.Length < 100) {
			GameObject newCube = Instantiate(theCube, new Vector3(0, 10f, 0), Quaternion.identity) as GameObject;
		}
	}
}
