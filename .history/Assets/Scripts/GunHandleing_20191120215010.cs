using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandleing : MonoBehaviour
{

    private Vector3 initialPosition;
    static Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        anim = gameObject.GetComponentInParent(Animator);
    }

    // Update is called once per frame
    void Update()
    {
         if (anim.GetCurrentAnimatorStateInfo(0).IsName("isWalking"))
            {
                Debug.Log("nice");
            }
    }
}
