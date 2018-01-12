using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {

    


    public GameObject target;
    public float speedMod = 3.0f;
    private Vector3 point;

    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);
    }

    void Update()
    {
        target.transform.Translate(0f, 0f, 0.5f);

        point = target.transform.position;
        transform.LookAt(point);
        transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
    }
}
