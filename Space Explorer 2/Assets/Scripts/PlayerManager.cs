using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	public GameObject canvas;

	public override void OnStartLocalPlayer(){
		GetComponentInChildren<Camera> ().enabled = true;
		GetComponentInChildren<AudioListener> ().enabled = true;
		GetComponentInChildren<Camera> ().transform.SetParent (null);

		canvas.SetActive (true);
	}
}
