using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [SerializeField]
    int maxHelath = 100;
    [SerializeField]
    int curHealth;
    [SerializeField]
    float regenarateTime = 2f;
    [SerializeField]
    int regenareateAmount = 10;

    [SerializeField]
    ShieldUI healthBar;

    void Start()
    {
        curHealth = maxHelath;
        InvokeRepeating("Regenerate", regenarateTime, regenarateTime);

    }

    void Regenerate()
    {

        if (curHealth < maxHelath)
        {
            curHealth += regenareateAmount;
            healthBar.UpdateShieldDisplay((float)(curHealth / (float)maxHelath));
        }
        if (curHealth > maxHelath)
        {
            curHealth = maxHelath;
            healthBar.UpdateShieldDisplay((float)(curHealth / (float)maxHelath));

        }        
    }

    public void TakeDamage(int dmg = 30)
    {
             
        curHealth -= dmg;
        healthBar.UpdateShieldDisplay((float)(curHealth/(float)maxHelath));

        if (curHealth < 1)
        {
            Debug.Log("WE ARE AT 0 HEALTH");
        }
    }
}
