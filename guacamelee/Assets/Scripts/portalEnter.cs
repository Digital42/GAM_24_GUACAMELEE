using UnityEngine;
using System.Collections;

public class portalEnter : MonoBehaviour {
	public GameObject World1;
	public GameObject World2;

	void Start(){
		World1 = GameObject.FindWithTag ("world1");
		World2 = GameObject.FindWithTag ("world2");
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("works");
		World1.SetActive(false);
		World2.SetActive(true);
	}

}
