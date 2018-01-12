using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 7f;
	public float turnSpeed = 5f;
	public float pitchSpeedLimit = 3f;
	public float jawSpeedLimit = 3f;
	public float rollSpeedLimit = 3f;
	public float boostPercentage = 100f;
	public float boostTime = 10f;

	Rigidbody rb;

	bool boost;
	float originalSpeed;
	float boostSpeed;
	float boostTimer;
	BoostUI boostUI;

	float verticalRotation;
	float horizontalRotation;
	float bankAngle;

	void Start () {
		
		rb = GetComponent<Rigidbody> ();
		boostUI = GameObject.Find ("BoostUI").GetComponent<BoostUI> ();
		originalSpeed = moveSpeed;
		boostSpeed = moveSpeed * (1 + (boostPercentage / 100));
		boostTimer = boostTime;
	}

	void Update(){

		boost = Input.GetKey(KeyCode.Space);
		if (boost && boostTimer > 0) {
			
			moveSpeed = boostSpeed;
			boostTimer -= Time.deltaTime;

		} else {
			
			moveSpeed = originalSpeed;
			if (boostTimer <= boostTime && !boost)
				boostTimer += Time.deltaTime/2;
		}
		boostUI.updateBoostBar (boostTimer / boostTime);

	}

	void FixedUpdate () {

		//kretanje broda
		//rikverc je 25% brzine
		float fwd = Input.GetAxis ("Vertical");
		if (fwd >= 0) {
			rb.AddForce (fwd * transform.forward * moveSpeed, ForceMode.Impulse);
		} else {
			rb.AddForce (fwd * transform.forward * moveSpeed * 0.25f, ForceMode.Impulse);
		}

		//rotacija broda
		float x = Input.GetAxis ("Mouse X");
		float y = Input.GetAxis ("Mouse Y");
		float z = Input.GetAxis ("Horizontal");
		horizontalRotation = x * turnSpeed;
		verticalRotation = y * turnSpeed;
		bankAngle = z * turnSpeed;

		//limitacija brzine rotiranja, bez limita vrti se kolko brzo pomices mis, s limitima ima odredjenu maksmimalnu brzinu rotacije
		verticalRotation = Mathf.Clamp (verticalRotation, -pitchSpeedLimit, pitchSpeedLimit);
		horizontalRotation = Mathf.Clamp (horizontalRotation, -jawSpeedLimit, jawSpeedLimit);
		bankAngle = Mathf.Clamp (bankAngle, -rollSpeedLimit, rollSpeedLimit);

		transform.Rotate (-verticalRotation, 0f, 0f, Space.Self);
		transform.Rotate (0f, horizontalRotation, 0f, Space.Self);
		transform.Rotate (0f, 0f, -bankAngle, Space.Self);
	}
}
