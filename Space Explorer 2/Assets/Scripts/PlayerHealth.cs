using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float maxShield = 100f;
	public float shieldRegenTime = 2f;
	public float shieldRegenAmount = 10f;
	public GameObject deathExplosion;

    GameObject shield;

	float currentHealth;
	float currentShield;
	bool isDead = false;
	HealthUI healthUI;

	float shieldBeforeHit;

	void Awake(){
		healthUI = GameObject.Find ("Health&Shield").GetComponent<HealthUI> ();
        shield = GameObject.Find("Shield");

	}

	void Start () {
		
		currentHealth = maxHealth;
		currentShield = maxShield;

        shield.SetActive(false);
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

		CancelInvoke ("RegenerateShield");
		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);

		shieldBeforeHit = currentShield;

		if (currentShield > 0) {
			currentShield -= dmg;

            shield.SetActive(true);
            Invoke("DeactivateShield", 1f);


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
		CancelInvoke ();
		Instantiate (deathExplosion, transform.position, transform.rotation);
		this.gameObject.SetActive (false);

		//Debug.Log("WE ARE AT 0 HEALTH");

	}

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

	public float getShield(){
		return currentShield;
	}

}
