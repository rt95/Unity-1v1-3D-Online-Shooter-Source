using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour
{

    Animator animator;
    public PlayerAim PlayerAim;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical, 0.2f, Time.deltaTime);
            animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal, 0.2f, Time.deltaTime);

            animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
            animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsSprinting);
            animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);

            animator.SetFloat("AimAngle", PlayerAim.GetAngle());
            animator.SetBool("IsAiming", GameManager.Instance.InputController.IsAiming);
        }
    }
}
