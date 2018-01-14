using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	public const float maxHealth = 100f;
	public const float maxShield = 100f;
	public const float shieldRegenTime = 2f;
	public const float shieldRegenAmount = 10f;
	public GameObject deathExplosion;

	[SerializeField]
	GameObject shield;

	[SyncVar(hook = "OnChangeHealth")]
	float currentHealth = maxHealth;
	[SyncVar(hook="OnChangeShield")]
	float currentShield = maxShield;
	[SerializeField]
	HealthUI healthUI;

	float shieldBeforeHit;

	void Start () {
		if (!isServer)
			return;
		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);
	}

	void RegenerateShield(){

		if (currentShield < maxShield) {
			currentShield += shieldRegenAmount;
			//healthUI.UpdateShieldBar (currentShield / maxShield);
		}
		if (currentShield > maxShield) {
			currentShield = maxShield;
			//healthUI.UpdateShieldBar (currentShield / maxShield);
		}

	}

	public void TakeDamage(float dmg){

		if (!isServer)
			return;

		CancelInvoke ("RegenerateShield");
		InvokeRepeating ("RegenerateShield", shieldRegenTime, shieldRegenTime);

		shieldBeforeHit = currentShield;

		if (currentShield > 0) {
			currentShield -= dmg;

			RpcFlipShield ();

            if (currentShield < 0) {
				currentShield = 0;
				dmg -= shieldBeforeHit;
				currentHealth -= dmg;
				//healthUI.UpdateHealthBar (currentHealth / maxHealth);

			}

			//healthUI.UpdateShieldBar (currentShield / maxShield);

		} else {
			currentHealth -= dmg;
			if (currentHealth < 0)
				currentHealth = 0;

			//healthUI.UpdateHealthBar (currentHealth / maxHealth);
		}

		if (currentHealth <= 0) {
			Death ();
		}
	}

	void Death(){
		
		CancelInvoke ();
		NetworkServer.Spawn ((GameObject)Instantiate (deathExplosion, transform.position, transform.rotation));
		RpcHideOnDeath ();

		//Debug.Log("WE ARE AT 0 HEALTH");

	}

    void DeactivateShield()
    {
        shield.SetActive(false);
    }

	public float getShield(){
		return currentShield;
	}

	void OnChangeHealth(float health){
		healthUI.UpdateHealthBar (health / maxHealth);
	}

	void OnChangeShield(float shield){
		healthUI.UpdateShieldBar (shield / maxShield);
	}

	[ClientRpc]
	void RpcHideOnDeath(){
		this.gameObject.SetActive (false);
	}

	[ClientRpc]
	void RpcFlipShield(){
		shield.SetActive (true);
		Invoke("DeactivateShield", 1f);
	}
}
