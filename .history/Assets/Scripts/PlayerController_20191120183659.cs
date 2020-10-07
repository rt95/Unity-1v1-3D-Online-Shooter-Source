using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    static Animator anim;
    public float speed = 1f;
    public float rotationSpeed = 100.0f;
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
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey("w"))
        {
            anim.SetBool("isWalking",true);
        }
        else {
            anim.SetBool("isWalking",false);
            anim.SetBool("isIdle",true);
        }

        if(Input.GetKey(KeyCode.LeftShift)){    //(translation != 0)
            anim.SetBool("isRunning",true);
            anim.SetBool("isIdle",false);
        }
        else{
            anim.SetBool("isRunning",false);
            anim.SetBool("isIdle",true);
        }
    }
}
