using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimation : MonoBehaviour
{
    public enum ETrapType { BuzzSaw, FloorGrinder, SwingingAxe }
    public ETrapType trapType;
    public GameObject pivot;
    public float moveSpeed;
    float baseMovespeed;
    private float direction = 1;
    private void Start()
    {
        baseMovespeed = moveSpeed;
    }
    void Update()
    {
        if (trapType == ETrapType.BuzzSaw)
        {
            pivot.transform.Translate(new Vector3(direction, 0, 0) * Time.deltaTime * moveSpeed, Space.Self);
            transform.Rotate(new Vector3(0, 0, 1) * moveSpeed);
            if (pivot.transform.localPosition.x > 2.5f && direction > 0)
            {
                direction = -direction;
            }
            else if (pivot.transform.localPosition.x < -2.5f && direction <0)
            {
                direction = -direction;
            }
        }else if(trapType == ETrapType.FloorGrinder)
        {
            transform.Rotate(new Vector3(0, 0, 1) * moveSpeed);
        }
       /* else if(trapType == ETrapType.SwingingAxe)
        {
            transform.Rotate(new Vector3(0, 0, direction) * moveSpeed);
            float secondBaseMS = baseMovespeed;
            if(transform.localEulerAngles.z > 30 && moveSpeed == baseMovespeed)
            {
                moveSpeed /= 2;
            }
            if (transform.localEulerAngles.z > 45 && moveSpeed/3 != baseMovespeed)
            {
                moveSpeed /= 3;
            }
        }*/
    }
}
