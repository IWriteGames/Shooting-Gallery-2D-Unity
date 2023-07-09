using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootImage : MonoBehaviour
{
    private float timeLifeShoot = 3f;

    private void Update()
    {
        timeLifeShoot -= Time.deltaTime;
        if(timeLifeShoot <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
