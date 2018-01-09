using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 10f;
	public float turnSpeed = 5f;

	Rigidbody rb;

	float verticalRotation;
	float horizontalRotation;
	float bankAngle;

	void Start () {
		
		rb = GetComponent<Rigidbody> ();

	}

	void FixedUpdate () {

		//kretanje broda
		float fwd = Input.GetAxis ("Vertical");
		//rade oba 2 nacina, preko rigidbodya se zbog sile lijepi za asteroide, al ima prirodnije kretanje (ubrzavanje, usporavanje, drift)
		//translate radi identicno ko na tvojoj skripti
		rb.AddForce (fwd * transform.forward * moveSpeed, ForceMode.Impulse);
		//transform.Translate(0, 0, fwd * moveSpeed * 5 * Time.deltaTime);


		//rotacija broda
		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");
		float z = Input.GetAxis ("Horizontal");
		horizontalRotation = x * turnSpeed;
		verticalRotation = y * turnSpeed;
		bankAngle = z * turnSpeed;

		//limitacija brzine rotiranja, bez limita vrti se kolko brzo pomices mis, s limitima ima odredjenu maksmimalnu brzinu rotacije
		verticalRotation = Mathf.Clamp (verticalRotation, -2f, 2f);
		horizontalRotation = Mathf.Clamp (horizontalRotation, -2f, 2f);
		bankAngle = Mathf.Clamp (bankAngle, -5f, 5f);

		transform.Rotate (-verticalRotation, 0f, 0f, Space.Self);
		transform.Rotate (0f, horizontalRotation, 0f, Space.Self);
		transform.Rotate (0f, 0f, -bankAngle, Space.Self);
	}
}
