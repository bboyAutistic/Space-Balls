using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileMP : NetworkBehaviour {

	public float moveSpeed = 10f;
	public float turnSpeed = 1f;
	public float timeOfFlight = 10f;
	public float damage = 100f;
	public GameObject explosion;

	Rigidbody rb;
	float timer;
	GameObject target;
	CapsuleCollider kolajder;

	void Awake () {
		rb = GetComponent<Rigidbody> ();
		target = GetComponentInParent<PlayerInputMP> ().getTarget ();
		transform.SetParent(null);
		kolajder = GetComponent<CapsuleCollider> ();
	}

	void Start(){
		timer = 0f;
		kolajder.enabled = false;
	}

	void Update(){

		timer += Time.deltaTime;

		if (timer > 2 && !kolajder.enabled)
			kolajder.enabled = true;

		if (timer > timeOfFlight) {
			NetworkServer.Spawn (Instantiate (explosion, transform.position, transform.rotation));
			Destroy (this.gameObject);
		}
	}

	void FixedUpdate () {
		rb.AddForce (transform.forward * moveSpeed, ForceMode.Impulse);

		if (target != null) {
			Quaternion newRotation = Quaternion.LookRotation (target.transform.position - transform.position);
			Quaternion rotation = Quaternion.RotateTowards (transform.rotation, newRotation, turnSpeed);
			transform.rotation = rotation;
		}
	}

	void OnCollisionEnter(Collision other){

		NetworkServer.Spawn (Instantiate (explosion, transform.position, transform.rotation));

		if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerHealthMP> ().TakeDamage (damage);
		} else if (other.gameObject.CompareTag ("Enemy")) {
			other.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
		}

		Destroy (this.gameObject);

	}

}
