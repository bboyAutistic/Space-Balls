using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    Laser[] laser;

	void Awake(){
		laser = GetComponentsInChildren<Laser> ();
	}

	void Update () {
       
		if (Input.GetKey(KeyCode.Mouse0))
        {
            foreach (Laser las in laser)
            {
                //Vector3 pos = transform.position + (transform.forward * l.Distance());
				if(las.getCanFire())
					las.FireLaser();
            }
            
        }
	}
}
