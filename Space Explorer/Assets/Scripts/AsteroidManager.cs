using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    [SerializeField]
    int numberOfAsteroidsOnAnAxis = 5;
    [SerializeField]
    int gridSpacing = 50;

    [SerializeField]
    Asteroid asteroid;

    void Start()
    {
        PlaceAsteroids();
    }

    void PlaceAsteroids()
    {
        for (int x = 0; x < numberOfAsteroidsOnAnAxis; x++)
        {
            for (int y = 0; y < numberOfAsteroidsOnAnAxis; y++)
            {
                for (int z = 0; z < numberOfAsteroidsOnAnAxis; z++)
                {
                    InstantiateAsteroid(x, y, z);
                }
            }
               
        }
    }


    void InstantiateAsteroid(int x, int y, int z)
    {
        Instantiate(  
                     asteroid, 
                     new Vector3(transform.position.x+(x* gridSpacing) + AsteroidOffset(),
                     transform.position.y + (y * gridSpacing) + AsteroidOffset(), 
                     transform.position.z + (z * gridSpacing)+ AsteroidOffset()), 
                     Quaternion.identity, 
                     transform);
    }

    float AsteroidOffset()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}
