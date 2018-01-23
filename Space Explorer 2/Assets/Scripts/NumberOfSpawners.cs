using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfSpawners : MonoBehaviour {

    public static int numberOfEnemis;
    public GameObject[] enemiSpawners;


    void Start()
    {     
        for (int i = 0; i < numberOfEnemis; i++)
        {
            enemiSpawners[i].SetActive(true);
        }
    }

    public void SetBrProtivnika(int br)
    {
        numberOfEnemis = br;
    }

}
