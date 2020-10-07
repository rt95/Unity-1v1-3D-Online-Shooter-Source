using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    //Ragdoll
    private Rigidbody[] bodyParts;
    public Animator animator;

    //DisableOnDeath
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private MouseLook mouseLook;
    [SerializeField]
    private GunController shooting;

    //ESC
    public GameObject menu; // Assign in inspector
    private bool isShowing = false;


    public void Setup()
    {
        //wasEnabled = new bool[disableOnDeath.Length];
        //for (int i = 0; i < wasEnabled.Length; i++)
        //{
        //    wasEnabled[i] = disableOnDeath[i].enabled;
        //}

        setDefaults();
    }

    void Awake()
    {
        //Ragdoll
        bodyParts = transform.GetComponentsInChildren<Rigidbody>();
        EnableRagdoll(false);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    RpcTakeDamage(999);
        //}

        if (GameManager.Instance.InputController.ESC)
        {
            Cursor.lockState = CursorLockMode.Confined;
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }

    public void EnableRagdoll(bool value)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].isKinematic = !value;
        }
    }

    public void setDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;

        EnableRagdoll(false);
        animator.enabled = true;
        movement.enabled = true;
        mouseLook.enabled = true;
        shooting.enabled = true;

        //for (int i = 0; i < disableOnDeath.Length; i++)
        //{
        //    disableOnDeath[i].enabled = wasEnabled[i];
        //}

        //Collider _col = GetComponent<Collider>();
        //if(_col != null)
        //{
        //    _col.enabled = true;
        //}
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead)
            return;

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        //Disable components
        //for (int i = 0; i < disableOnDeath.Length; i++)
        //{
        //    disableOnDeath[i].enabled = false;
        //}

        //Collider _col = GetComponent<Collider>();
        //if (_col != null)
        //{
        //    _col.enabled = false;
        //}

        Debug.Log(transform.name + " is dead !");

        //Ragdoll
        EnableRagdoll(true);
        animator.enabled = false;
        movement.enabled = false;
        mouseLook.enabled = false;
        shooting.enabled = false;

        StartCoroutine(Respawn());

        //Win
        GameManager.Instance.Win(transform.name);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f); /*GameManager.Instance.matchSettings.respawnTime*/

        setDefaults();
        Transform _SpawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _SpawnPoint.position;
        transform.rotation = _SpawnPoint.rotation;
    }
}
