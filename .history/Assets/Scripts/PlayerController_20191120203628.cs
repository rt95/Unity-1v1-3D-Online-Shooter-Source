﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    static Animator anim;
    public float speed = 1f;
    public float sideWaysSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float forwardMotion = Input.GetAxis("Vertical") * speed;
        float sidewaysMotion = Input.GetAxis("Horizontal") * sideWaysSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        forwardMotion *= Time.deltaTime;
        sidewaysMotion *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, forwardMotion);

        // Rotate around our y-axis
        transform.Translate(sidewaysMotion, 0, 0);

        //(forwardMotion != 0)

        if(Input.GetKey("w"))
        {  
            anim.SetBool("isWalking",true);
        }else
        {
            anim.SetBool("isWalking",false);
            anim.SetBool("isIdle",true);
        }

        if(Input.GetKey("s"))
        {  
            anim.SetBool("isWalkingBack",true);
        }else
        {
            anim.SetBool("isWalkingBack",false);
            anim.SetBool("isIdle",true);
        }

        if(Input.GetKey("a"))
        {  
            anim.SetBool("isStrafingLeft",true);
        }else
        {
            anim.SetBool("isStrafingLeft",false);
            anim.SetBool("isIdle",true);
        }

        if(Input.GetKey("d"))
        {  
            anim.SetBool("isStrafingRight",true);
        }else
        {
            anim.SetBool("isStrafingRight",false);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {  
            anim.SetBool("isRunning",true);
            anim.SetBool("isIdle",false);
            speed = 3f;
        }else
        {
            anim.SetBool("isRunning",false);
            anim.SetBool("isIdle",true);
            speed = 1f;
        }

        if (Input.GetMouseButton(1))
        {  
            anim.SetBool("isAiming",true);
        }else
        {
            anim.SetBool("isAiming",false);
            anim.SetBool("isIdle",true);
        }
    }
}
