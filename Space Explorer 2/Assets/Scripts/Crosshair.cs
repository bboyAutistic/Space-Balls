using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour {

	public GameObject player;
	public RectTransform crosshair;

	Ray ray;

	void Update(){
		ray.origin = player.transform.position;
		ray.direction = player.transform.forward;

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 300f))
			crosshair.position = Camera.main.WorldToScreenPoint (hit.point);
		else
			crosshair.position = Camera.main.WorldToScreenPoint (ray.GetPoint (300f));
	}

}
