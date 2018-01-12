using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Explozions : MonoBehaviour {
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    Rigidbody rb;
    [SerializeField] float laserHitModifier=100f;

    [SerializeField]
    float particleDuration = 5f;

    public void IvebeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, (particleDuration + 1f));
    }

   
    public void AddForceAfterHit(Vector3 hitPosition,Transform hitSource)
    {
        if (rb == null) return;
        Vector3 direction = (hitSource.position - hitPosition).normalized;
        rb.AddForceAtPosition(direction * laserHitModifier, hitPosition, ForceMode.Impulse);
    }

}
