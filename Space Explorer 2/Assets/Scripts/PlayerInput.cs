using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

    Laser[] laser;
	public GameObject missile;
	public float lockOnRange = 50f;
	public float missileReloadTime = 5f;
	public float lockOnTime = 5f;

	GameObject target = null;
	int lockTarget = 0;
	float nearestTarget;
	float missileReloadTimer = 0f;
	float lockTimer = 0f;

	bool lockOn = false;

	public RectTransform targetLockUI;
	Color targetLockUIColor;

	void Awake(){
		laser = GetComponentsInChildren<Laser> ();
		target = null;
		GetComponent<SphereCollider> ().radius = lockOnRange;
		targetLockUIColor = targetLockUI.GetComponent<Image> ().color;
	}

	void Update () {

        if (Time.timeScale == 0) { return; }

		//reload raketa i lock-on
		missileReloadTimer += Time.deltaTime;
		if (lockTimer > lockOnTime)
			lockOn = true;
		else
			lockOn = false;

		//pucanje lasera
		if (Input.GetKey(KeyCode.Mouse0))
        {
            foreach (Laser las in laser)
            {
				if(las.getCanFire())
					las.FireLaser();
            }
            
        }

		//update lock-on UI
		if (target != null) {
			//Debug.Log (lockTimer);
			targetLockUI.gameObject.SetActive (true);
			UpdateTargetLockUI ();
		} else {
			targetLockUI.gameObject.SetActive (false);
		}

		//pucanje raketa
		if (Input.GetKeyDown (KeyCode.Mouse1) && target != null && missileReloadTimer > missileReloadTime && lockOn) {
			missileReloadTimer = 0f;
			Instantiate (missile, transform.position - transform.up, transform.rotation, transform);
		} else if (Input.GetKeyDown (KeyCode.Mouse1) && target == null && missileReloadTimer > missileReloadTime) {
			missileReloadTimer = 0f;
			Instantiate (missile, transform.position - transform.up, transform.rotation, transform);
		}
	}

	//geter targeta kojeg raketa prati
	public GameObject getTarget(){
		return target;
	}

	void OnTriggerStay(Collider other){
		//gledaj samo za Enemy
		if (other.CompareTag ("Enemy")) {
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			//ako je unutar 30 stupnjeva
			if (angle <= 30f) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, direction, out hit, lockOnRange)) {
					if (hit.collider.gameObject.CompareTag ("Enemy")) {

						//when no target, set a target
						if (lockTarget == 0 || target == null) {
							lockTarget = hit.collider.gameObject.GetInstanceID ();
							nearestTarget = hit.distance;
							lockTimer = 0f;
							target = hit.collider.gameObject;
							Debug.Log ("NEW TARGET");
						}

						//if same target, add to timer, update distance
						else if (lockTarget == hit.collider.gameObject.GetInstanceID ()) {
							lockTimer += Time.deltaTime;
							nearestTarget = hit.distance;
							if (target != hit.collider.gameObject) {
								target = hit.collider.gameObject;
								Debug.Log ("NEW TARGET");
							}
						}

						//if new target is closer, change target
						else if (lockTarget != hit.collider.gameObject.GetInstanceID () && hit.distance < nearestTarget) {
							lockTimer = 0f;
							lockTarget = hit.collider.gameObject.GetInstanceID ();
							nearestTarget = hit.distance;
						}

					}
					//if no line of sight, reset target
					else {
						ResetTarget ();
						Debug.Log ("Target lost - line of sight");
					}
				}
			}
		}

		//if target was found
		if (target != null) {
			//if target left the area, reset target
			if (Vector3.Angle (target.transform.position - transform.position, transform.forward) > 30f) {
				ResetTarget ();
				Debug.Log ("Target lost - not in area");
			}
		}
	}
		
	void ResetTarget(){
		lockTarget = 0;
		lockTimer = 0f;
		target = null;
	}
		
	void UpdateTargetLockUI(){
		targetLockUI.position = Camera.main.WorldToScreenPoint (target.transform.position);

		float size = 500 / (lockTimer * 2);
		size = Mathf.Clamp (size, 50f, 500f);

		targetLockUI.sizeDelta = new Vector2 (size, size);
		if (size == 50) {
			targetLockUI.GetComponent<Image> ().color = Color.red;
		} else {
			targetLockUI.GetComponent<Image> ().color = targetLockUIColor;
		}
	}
}
