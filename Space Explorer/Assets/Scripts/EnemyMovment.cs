using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    float rotationalDamp = 0.5f;

    [SerializeField]
    float movementSpeed = 10f;

    void Update()
    {
        if (!FindTarget()) return;
        Turn();
        Move();
        
    }

    

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,  rotationalDamp*Time.deltaTime);
    }
    void Move()
    {
        //provjerava jel u X(br) polja blizini, ako je usporva, ako ne ubrzava
        if ((target.transform.position - this.transform.position).sqrMagnitude < 5 * 5)
        {

            //Debug.Log("usporava");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 0.01f;

        }
        else if ((target.transform.position - this.transform.position).sqrMagnitude < 30 * 30)
        {

            //Debug.Log("usporava");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 0.4f;

        }
        else
        {

            //Debug.Log("brzooo");
            transform.position += transform.forward * Time.deltaTime * movementSpeed * 1.2f;
        }
    }

    bool FindTarget()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null) return false;
        return true;
    }
}
