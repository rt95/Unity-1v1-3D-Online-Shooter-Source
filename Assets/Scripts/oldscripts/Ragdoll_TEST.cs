using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll_TEST : MonoBehaviour
{
    private Rigidbody[] bodyParts;
    public Animator animator;

    void Start()
    {
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }

    public void EnableRagdoll(bool value)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }

    [ContextMenu("Test Ragdoll!")]
    public void TestRagdoll()
    {
        EnableRagdoll(true);
        animator.enabled = false;
    }
}
