using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour {

	float timer;
	void Start () {
		timer = 0f;
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > 2f)
			Destroy (this.gameObject);
	}
}
