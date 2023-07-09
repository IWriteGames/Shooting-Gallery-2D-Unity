using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] private bool positionWater;
    
    private float timeCountAnimation;

    private void Update() 
    {
        timeCountAnimation += Time.deltaTime;

        if(positionWater)
        {
            transform.position += new Vector3(2f, 0f, 0f) * Time.deltaTime;
        }

        if(!positionWater)
        {
            transform.position += new Vector3(-2f, 0f, 0f) * Time.deltaTime;
        }

        if(timeCountAnimation > 5f)
        {
            positionWater = !positionWater;
            timeCountAnimation = Time.deltaTime;
        }
    }
}
