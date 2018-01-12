using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class Laser : MonoBehaviour {

    public float laserOffTime = 0.05f;
    public float maxDistance = 300f;
	public float fireDelay = 0.8f;
	public float damage = 30f;

	LineRenderer lr;
    bool canFire;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lr.enabled = false;
        canFire=true;
    }
    
    void Update()
    {
        //na ekranu se pojavi ray koji je vidljiv cijelo vrijeme
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance,Color.blue);
		//timer+=Time.deltaTime;
		//if (Input.GetKey(KeyCode.Mouse0) && gameObject.name == "Laser" && timer > fireDelay) {
		//	FireLaser ();
		//}
    }

    private Vector3 CastRay()
    {
        RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, maxDistance)) {

			if (hit.collider.gameObject.CompareTag ("Asteroid")) {
				SpawnExplosion(hit.point, hit.transform);
			}

			if (hit.collider.gameObject.CompareTag ("Player")) {
				//PlayerHealth player = hit.collider.gameObject.GetComponent<PlayerHealth> ();
				PlayerHealth player = hit.collider.gameObject.GetComponent<PlayerHealth> ();
				player.TakeDamage (damage);
				if (player.getShield() <= 0) {
					SpawnExplosion(hit.point, hit.transform);
				}
			}

			if (hit.collider.gameObject.CompareTag ("Enemy")) {
				hit.collider.gameObject.GetComponent<EnemyHealth> ().TakeDamage (damage);
				SpawnExplosion (hit.point, hit.transform);
			}

			return hit.point;

		}

		Debug.Log("We missed ");
		return transform.position + (transform.forward * maxDistance);

    }

    void SpawnExplosion(Vector3 hitPosition,Transform target)
    {
        Explozions tempExploz = target.transform.GetComponent<Explozions>();
        if (tempExploz != null)
        {
            tempExploz.IvebeenHit(hitPosition);
            tempExploz.AddForceAfterHit(hitPosition, transform);
        }
            
    }



    //poziva overloudan samo kaj ovaj radi za Playera
    public void FireLaser()
    {
        FireLaser(CastRay());

    }

    //overloudan tako da radi i za Enemy
    public void FireLaser(Vector3 targetPostion,Transform target=null)
    {
            if(target != null)
            {
				PlayerHealth playerShotByAI = target.gameObject.GetComponent<PlayerHealth> ();
				playerShotByAI.TakeDamage (damage);
				if (playerShotByAI.getShield () <= 0) {
					SpawnExplosion(targetPostion, target);
				}
            }
                
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPostion);
            lr.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        
    }

    public float Distance()
    {
       return maxDistance; 
    }

    void CanFire()
    {
        canFire = true; 

    }

	public bool getCanFire(){
		return canFire;
	}

}
