﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 7f;
	public float turnSpeed = 5f;
	public float pitchSpeedLimit = 3f;
	public float jawSpeedLimit = 3f;
	public float rollSpeedLimit = 3f;

	Rigidbody rb;

	float verticalRotation;
	float horizontalRotation;
	float bankAngle;

	void Start () {
		
		rb = GetComponent<Rigidbody> ();

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
		//transform.Translate(0, 0, fwd * moveSpeed * 5 * Time.deltaTime);


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
