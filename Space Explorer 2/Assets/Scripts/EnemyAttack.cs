using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    Transform target;

    [SerializeField]
    Laser laser;

    Vector3 hitPosition;

    void Update()
    {
        if (!FindTarget()) return;
        InFront();
        HaveLineOfSightRayCast();
        if(InFront() && HaveLineOfSightRayCast())
        {
            FireEnemyLaser();
            
        }
    }



	bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (Mathf.Abs(angle) > 120 && Mathf.Abs(angle) < 300)
        {
           // Debug.DrawLine(transform.position, target.position,Color.green);
            return true;
        }
        //Debug.DrawLine(transform.position, target.position, Color.red);
        return false;
    }


    bool HaveLineOfSightRayCast()
    {
        RaycastHit hit;

        Vector3 direction = target.position - transform.position;
        
        if(Physics.Raycast(laser.transform.position,direction,out hit,laser.Distance()))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(laser.transform.position, direction, Color.green);
                hitPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }


    void FireEnemyLaser()
    {
        //Debug.Log("fire Laseeerrrrrrrr");
        laser.FireLaser(hitPosition,target);
    }

    bool FindTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) return false;
        return true;
    }
}
