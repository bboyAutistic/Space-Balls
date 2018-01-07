using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    [SerializeField]
    float minScale = 32f;
    [SerializeField]
    float maxScale = 45f;

    [SerializeField]
    float rotationoffset = 50f;

    

    Transform myT;

    Vector3 randomRotation;

	void Awake()
    {
        myT = transform;
    }

    void Start()
    {
        //random size
        Vector3 scale= Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);

        myT.localScale = scale;

        //random rotation
        randomRotation.x = Random.Range(-rotationoffset, rotationoffset);
        randomRotation.y = Random.Range(-rotationoffset, rotationoffset);
        randomRotation.z = Random.Range(-rotationoffset, rotationoffset);

    }

	void Update () {
        myT.Rotate(randomRotation * Time.deltaTime);
	}


   
}
