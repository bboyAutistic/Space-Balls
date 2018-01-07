using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float rotateSpeed = 5;
    public float distanceLerp = 12f;

    Vector3 offset;

    float horizontal = 0f;
    float vertical = 0f;





    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        horizontal = Mathf.Clamp(horizontal, -90f, 90f);
        target.transform.Rotate(0, horizontal, 0);


        float desiredAngleY = target.transform.eulerAngles.y;

        vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        vertical = Mathf.Clamp(vertical, -60f, 60f);
        target.transform.Rotate(-vertical, 0, 0);


        float desiredAngleX = target.transform.eulerAngles.x;


        Quaternion rotation = Quaternion.Euler(desiredAngleX, desiredAngleY, 0);

        //novo
        Vector3 toPos= target.transform.position - (rotation * offset);
        Vector3 curPos = Vector3.Lerp(transform.position, toPos, distanceLerp * Time.deltaTime);
        transform.position = curPos;
        //transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}