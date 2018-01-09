using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public GameObject target;

	Vector3 offset;

	void Start () {
		
		offset = transform.position - target.transform.position;

	}

	void FixedUpdate () {

		//oba 2 nacina rade
		//kamera zaostaje za spaceshipom ovisno o brzini, al rotacija ga prati uzastopno, teze je vidit kam ides kod skretanja
		//ako se kretanje ne radi preko rigidbodya kamera ne zaostaje
		//transform.rotation = target.transform.rotation;
		//Vector3 offsetTemp = offset + offset * (0.005f * target.GetComponent<Rigidbody> ().velocity.magnitude);
		//transform.position = target.transform.position + (target.transform.rotation * offsetTemp);


		//ovaj dio radi super
		transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, 15f * Time.deltaTime);
		Vector3 tarPos = target.transform.position + (transform.rotation * offset);
		Vector3 curPos = Vector3.Lerp(transform.position, tarPos, 10f * Time.deltaTime);
		transform.position = curPos;

	}
}
