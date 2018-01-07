using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Explozions : MonoBehaviour {
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    float particleDuration = 5f;

    public void IvebeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, (particleDuration + 1f));
    }
}
