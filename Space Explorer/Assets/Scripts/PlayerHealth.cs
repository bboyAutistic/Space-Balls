using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float maxShield = 100f;
	public float shieldRegenTime = 2f;
	public float shieldRegenAmount = 10f;

	float currentHealth;
	float currentShield;
	bool isDead = false;

	HealthUI healthUI;

	float shieldBeforeHit;

	void Awake(){
		healthUI = GameObject.Find ("Health&Shield").GetComponent<HealthUI> ();
	}

	void Start () {
		
		currentHealth = maxHealth;
		currentShield = maxShield;

		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);
	}

	void RegenerateShield(){

		if (currentShield < maxShield) {
			currentShield += shieldRegenAmount;
			healthUI.UpdateShieldBar (currentShield / maxShield);
		}
		if (currentShield > maxShield) {
			currentShield = maxShield;
			healthUI.UpdateShieldBar (currentShield / maxShield);
		}

	}

	public void TakeDamage(float dmg){

		shieldBeforeHit = currentShield;

		if (currentShield > 0) {
			currentShield -= dmg;

			if (currentShield < 0) {
				currentShield = 0;
				dmg -= shieldBeforeHit;
				currentHealth -= dmg;
				healthUI.UpdateHealthBar (currentHealth / maxHealth);
			}

			healthUI.UpdateShieldBar (currentShield / maxShield);

		} else {
			currentHealth -= dmg;
			if (currentHealth < 0)
				currentHealth = 0;

			healthUI.UpdateHealthBar (currentHealth / maxHealth);
		}

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

	void Death(){
		
		isDead = true;
		Debug.Log("WE ARE AT 0 HEALTH");

	}
}
