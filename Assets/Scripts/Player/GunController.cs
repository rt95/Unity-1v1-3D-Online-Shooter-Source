using Mirror;
using System.Collections;
using UnityEngine;

public class GunController : NetworkBehaviour
{
    public float range = 100f;
    public float impactForce = 300f;
    public float fireRate = 5f;
    public int bullets = 30;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject lineRenderer;

    //public Transform hand;

    [SerializeField]AudioControllerWeapon audioReload;
    [SerializeField]AudioControllerWeapon audioFire;
    [SerializeField]LayerMask mask; //Control what we hit

    private float nextTimeToFire = 0f;

    int damage = 50;

    //public override void OnStartClient()
    //{
    //    Debug.Log("Weapon spawned! ");
    //}

    //void Awake()
    //{
    //    transform.SetParent(hand);
    //}

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            if (GameManager.Instance.InputController.Fire1 && Time.time >= nextTimeToFire)
            {
                if (isReloading)
                    return;
                nextTimeToFire = Time.time + 5f / fireRate;
                Shoot();
            }
            if (GameManager.Instance.InputController.Reload)
            {
                if (bullets == 30)
                    return;

                Reload();
            }
        }
    }

    void Reload()
    {
        if (isReloading)
            return;

        audioReload.Play();
        StartCoroutine(Reload_Corutine());
    }

    private IEnumerator Reload_Corutine()
    {

        isReloading = true;

        yield return new WaitForSeconds(5f);

        bullets = 30;

        isReloading = false;
    }

    [Client]
    void Shoot()
    {
        if (bullets <= 0)
        {
            Debug.Log("Out of bullets");
            return;
        }


        muzzleFlash.Play();
        audioFire.Play();

        bullets--;

        Debug.Log("Remaining bullets" + bullets);

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hit, range))
        {

        Debug.Log(hit.transform.tag);

        //PlayerHealth target = hit.transform.GetComponent(typeof(PlayerHealth)) as PlayerHealth;

        //if (target != null){
        //target.TakeDamage(hit.transform.tag);
        if (hit.transform.tag == "Player")
                {
                    CmdPlayerShot(hit.collider.name, damage);
                }
        //}

            if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 2f);

        //LineRenderer line = lineRenderer.GetComponent<LineRenderer>();

        //line.SetPosition(0, fpsCam.transform.position);
        //line.SetPosition(1, hit.point);

        //GameObject lineGO = Instantiate(lineRenderer, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(lineGO, 0.5f);
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + "has been shot");
        PlayerController _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }
}
