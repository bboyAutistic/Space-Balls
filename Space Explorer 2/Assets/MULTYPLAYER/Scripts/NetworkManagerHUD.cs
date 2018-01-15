using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerHUD : MonoBehaviour {

	public NetworkManager manager;
	public Text ipAddressText;

	void Awake(){
		manager = GetComponent<NetworkManager> ();
	}

	public void OnHostPress(){
		manager.StartHost ();
	}

	public void OnJoinPress(){
		manager.networkAddress = ipAddressText.text;
		manager.networkPort = 7777;
		manager.StartClient ();
	}

}
