using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public GameObject deathExplosion;

	float currentHealth;

	void Awake(){
		currentHealth = maxHealth;
	}

	public void TakeDamage(float dmg){
		currentHealth -= dmg;

		if (currentHealth <= 0)
			Death ();
	}

	void Death(){
		Instantiate (deathExplosion, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
