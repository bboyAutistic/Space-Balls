using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {

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

		if (!isLocalPlayer)
			return;

		//reload raketa i lock-on
		missileReloadTimer += Time.deltaTime;
		if (lockTimer > lockOnTime)
			lockOn = true;
		else
			lockOn = false;

		//pucanje lasera
		if (Input.GetKey(KeyCode.Mouse0))
        {
            /*foreach (Laser las in laser)
            {
				if (las.getCanFire ()) {
					las.FireLaser ();
					CmdFireLaser (las.transform.position, las.transform.forward, las.maxDistance, las.damage);
				}
            }*/

			for (int i = 0; i < laser.Length; i++) {
				if (laser [i].getCanFire) {
					CmdFireLaser (laser [i].transform.position, laser [i].transform.forward, laser [i].maxDistance, laser [i].damage);
				}
			}

        }

		//update lock-on UI
		if (target != null) {
			Debug.Log (lockTimer);
			targetLockUI.gameObject.SetActive (true);
			UpdateTargetLockUI ();
		} else {
			targetLockUI.gameObject.SetActive (false);
		}

		//pucanje raketa
		if (Input.GetKeyDown (KeyCode.Mouse1) && target != null && missileReloadTimer > missileReloadTime && lockOn) {
			missileReloadTimer = 0f;
			CmdFireMissile ();
		} else if (Input.GetKeyDown (KeyCode.Mouse1) && target == null && missileReloadTimer > missileReloadTime) {
			missileReloadTimer = 0f;
			CmdFireMissile ();
		}
	}

	//geter targeta kojeg raketa prati
	public GameObject getTarget(){
		return target;
	}

	void OnTriggerStay(Collider other){
		//gledaj samo za Enemy i drugog Playera
		if (other.CompareTag ("Enemy") || other.CompareTag("Player")) {
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			//ako je unutar 30 stupnjeva
			if (angle <= 30f) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position, direction, out hit, lockOnRange)) {
					if (hit.collider.gameObject.CompareTag ("Enemy") || hit.collider.gameObject.CompareTag("Player")) {

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

	[Command]
	void CmdFireMissile(){
		NetworkServer.Spawn((GameObject) Instantiate (missile, transform.position - transform.up, transform.rotation, transform));
	}

	[Command]
	void CmdFireLaser(Vector3 origin, Vector3 direction, float maxDistance, float damage){
		RaycastHit hit;
		if (Physics.Raycast (origin, direction, out hit, maxDistance)) {

			if (hit.collider.gameObject.CompareTag ("Asteroid")) {
				//SpawnExplosion(hit.point, hit.transform);
			}

			if (hit.collider.gameObject.CompareTag ("Player")) {
				//PlayerHealth player = hit.collider.gameObject.GetComponent<PlayerHealth> ();
				PlayerHealth player = hit.collider.gameObject.GetComponent<PlayerHealth> ();
				player.TakeDamage (damage);
				if (player.getShield () <= 0) {
					//SpawnExplosion(hit.point, hit.transform);
				}
			}

			if (hit.collider.gameObject.CompareTag ("Enemy")) {
				hit.collider.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
				//SpawnExplosion (hit.point, hit.transform);
			}

			//RpcShowLaser (hit.point);

		} else {
			//RpcShowLaser (origin + (direction * maxDistance));
		}
	}

	[ClientRpc]
	void RpcShowLaser(Vector3 hitPoint){
		
				las.FireLaser(hitPoint);
		
	}
}
