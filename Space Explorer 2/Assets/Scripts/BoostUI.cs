using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostUI : MonoBehaviour {

	public RectTransform boostBar;

	float maxHeight;

	void Awake(){
		maxHeight = boostBar.rect.height;
	}
	

	public void updateBoostBar(float percent){
		boostBar.sizeDelta = new Vector2 (15f, maxHeight * percent);
	}
}
