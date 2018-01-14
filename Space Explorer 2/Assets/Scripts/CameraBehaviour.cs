using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public GameObject target;
    

    public float distanceLerp = 15f;


    Vector3 offset;

	void Start () {
		
		offset = transform.position - target.transform.position;

	}

	void FixedUpdate () {

		transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, 15f * Time.deltaTime);
		Vector3 tarPos = target.transform.position + (transform.rotation * offset);
		Vector3 curPos = Vector3.Lerp(transform.position, tarPos, distanceLerp * Time.deltaTime);
		transform.position = curPos;

	}
    //cursor lock
    
	void Update(){

		//cursor lock
		if (Input.GetMouseButton (0))
			Cursor.lockState = CursorLockMode.Locked;
		if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;

	}
    
}
