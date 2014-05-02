using UnityEngine;
using System.Collections;

public class makeObjects : MonoBehaviour {

	GameObject[] targetList;
	public GameObject newCube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		targetList = GameObject.FindGameObjectsWithTag("Respawn");
		//Debug.Log(targetList.Length);

		if (targetList.Length < 5) {
			GameObject newCube = Instantiate("prefab_cube", Vector3.zero, Quaternion.identity) as GameObject;
		}
	}
}
