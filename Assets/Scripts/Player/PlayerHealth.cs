using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    //For ragdoll
    private Rigidbody[] bodyParts;
    public Animator animator;

    private PlayerMovement movement;
    private PlayerAim aim;
    private PlayerShoot shoot;
    private PlayerAnimation anim;

    ////Health
    //public float maxHealth = 100f;

    //[SyncVar]
    public float CurrentHealth = 100f;

    public float DamageAmount_HEAD = 100f;
    public float DamageAmount_TORSO = 50;
    public float DamageAmount_LEGS = 30f;
    public float DamageAmount_ARMS = 30f;

    //Respawn points
    //public GameObject respawnPrefab;
    //public GameObject[] spawnPoints;

    //[SyncVar]
    //private bool _isDead = false;
    //public bool isDead
    //{
    //    get { return _isDead; }
    //    protected set { _isDead = value; }
    //}


    void Awake()
    {
        //Ragdoll
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }

    void Start()
    {
        //isDead = false;
        //CurrentHealth = 100f;
        //animator.enabled = true;
        //movement.enabled = true;
        //aim.enabled = true;
        //shoot.enabled = true;
        //anim.enabled = true;

        //spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        //foreach (GameObject respawn in spawnPoints)
        //{
        //    Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
        //}
    }

    //Ragdoll

    public void EnableRagdoll(bool value)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }

    //[ContextMenu("Test Ragdoll!")]
    //void TestRagdoll()
    //{
    //    EnableRagdoll(true);
    //    animator.enabled = false;
    //}

    //Spawns

    //void SpawnAtNewSpawn()
    //{
    //    int spawnIndex = Random.Range(0, spawnPoints.Length);
    //    transform.position = spawnPoints[spawnIndex].transform.position;
    //    transform.rotation = spawnPoints[spawnIndex].transform.rotation;
    //}

    //[ContextMenu("Test Die!")]
    //void TestDie()
    //{
    //    SpawnAtNewSpawn();
    //}

    //[ClientRpc]
    public void TakeDamage(string tag)
    {

        //if (isDead)
        //    return;

        if (tag == "Player")
        {
            CurrentHealth -= DamageAmount_TORSO;
            //Debug.Log("PlayerHP " + CurrentHealth);
            if (CurrentHealth <= 0f)
            {
                Die();   
            }
        }

        if (tag == "Head")
        {
            CurrentHealth -= DamageAmount_HEAD;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        if (tag == "Torso")
        {
            CurrentHealth -= DamageAmount_TORSO;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        if (tag == "Legs")
        {
            CurrentHealth -= DamageAmount_LEGS;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }
        if (tag == "Arms")
        {
            CurrentHealth -= DamageAmount_ARMS;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        //isDead = true;

        //Disable components
        EnableRagdoll(true);
        animator.enabled = false;
        movement.enabled = false;
        aim.enabled = false;
        shoot.enabled = false;
        anim.enabled = false;

        //GameManager.Instance.Win();
    }

}
