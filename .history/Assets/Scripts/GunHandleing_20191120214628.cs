using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandleing : MonoBehaviour
{

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
         if (Animator.GetCurrentAnimatorStateInfo(0).IsName("isWalking"))
            {
                debug.log("nice");
            }
    }
}
