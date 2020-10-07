﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetRotation(float amount){
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - amount, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public float GetAngle()
    {
        return CheckAngle(transform.eulerAngles.x);
    }

    public float CheckAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }

}