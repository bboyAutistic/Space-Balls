using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour {

	public RectTransform healthBar;
	public RectTransform shieldBar;

	float maxWidthHealth;
	float maxWidthShield;

	void Awake () {

		maxWidthHealth = healthBar.rect.width;
		maxWidthShield = shieldBar.rect.width;

	}

	public void UpdateHealthBar (float percent){
		healthBar.sizeDelta = new Vector2 (maxWidthHealth * percent, 10f);
	}

	public void UpdateShieldBar(float percent){
		shieldBar.sizeDelta = new Vector2 (maxWidthShield * percent, 10f);
	}
}
