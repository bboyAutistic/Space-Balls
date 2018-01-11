using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class Laser : MonoBehaviour {

    [SerializeField]
    float laserOffTime = 0.05f;
    [SerializeField]
    float maxDistance = 300f;
    LineRenderer lr;

    [SerializeField]
    float fireDelay = 0.8f;
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
    /*
    void Update()
    {
        //na ekranu se pojavi ray koji je vidljiv cijelo vrijeme
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance,Color.blue);
    }
    */
    private Vector3 CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;
        if(Physics.Raycast(transform.position,fwd,out hit))
        {
            Debug.Log("We hit: " + hit.transform.name);

            SpawnExplosion(hit.point, hit.transform);

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
        if (canFire)
        {
            if(target != null)
            {
                SpawnExplosion(targetPostion, target);
                
            }
                
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPostion);
            lr.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
        }
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

}
