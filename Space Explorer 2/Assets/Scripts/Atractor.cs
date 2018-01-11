using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atractor : MonoBehaviour {



    public float G = 0.6674f;

    public static List<Atractor> Attractors;

    public Rigidbody rb;

    void FixedUpdate()
    {
        
        foreach(Atractor attractor in Attractors)
        {
            if(attractor != this)
            Attract(attractor);
        }
    }
    void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<Atractor>();
        Attractors.Add(this);

    }
    void OnDisable()
    {
        Attractors.Remove(this);
    }

	void Attract(Atractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;

        float distance = direction.magnitude;

        if (distance == 0f) return;

        float forceMagnitude = G*(rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);

        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
