using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public float DamageAmount_HEAD = 100f;
    public float DamageAmount_TORSO = 35;
    public float DamageAmount_LEGS = 30f;
    public float DamageAmount_ARMS = 30f;

    public void TakeDamage(string tag){

        if (tag == "Player")
        {
            health -= DamageAmount_TORSO;
            if (health <= 0f)
            {
                Die();
            }
        }

        if (tag == "Head")
        {
            health -= DamageAmount_HEAD;
            if (health <= 0f)
            {
                Die();
            }
        }

        if (tag == "Torso")
        {
            health -= DamageAmount_TORSO;
            if (health <= 0f)
            {
                Die();
            }
        }

        if (tag == "Legs")
        {
            health -= DamageAmount_LEGS;
            if (health <= 0f)
            {
                Die();
            }
        }
        if (tag == "Arms")
        {
            health -= DamageAmount_ARMS;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die() {
        Ragdoll_TEST ragdoll = GetComponent<Ragdoll_TEST>();
        ragdoll.TestRagdoll();
        if (health <= 0f)
        {
            //GameManager.Instance.Win();
        }
    }

}
