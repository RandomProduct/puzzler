using UnityEngine;
using System.Collections;

public class SphereHandler : MonoBehaviour {

	GameObject myTile = null;

	void OnTriggerEnter(Collider other){
		Debug.Log ("hiii");
		if (myTile == null) {
			myTile = other.gameObject;
		}
	}
	
}