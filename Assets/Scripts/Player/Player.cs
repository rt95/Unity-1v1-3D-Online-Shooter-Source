using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(PlayerController))]
public class Player : NetworkBehaviour
{

    public GameObject playerCamera;
    public GameObject Weapon;
    public GameObject vcam;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    //[SerializeField]
    //Behaviour[] componentsToDisable;

    //[System.Serializable]
    //public class MouseInput
    //{
    //    public Vector2 Damping;
    //    public Vector2 Sensitivity;
    //}

    //[SerializeField] float speed;
    //[SerializeField] MouseInput MouseControl;

    //private MoveController _moveController;
    //public MoveController MoveController
    //{
    //    get
    //    {
    //        if (_moveController == null)
    //            _moveController = GetComponent<MoveController>();
    //        return _moveController;
    //    }
    //}

    //InputController playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        //playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
        GetComponent<PlayerController>().Setup();
    }

    void Start() {
        //if (!isLocalPlayer)
        //{
        //    for (int i = 0; i < componentsToDisable.Length; i++)
        //    {
        //        componentsToDisable[i].enabled = false;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        //MoveController.Move(direction);

        if (isLocalPlayer)
        {
            playerCamera.SetActive(true);
            //Weapon.SetActive(true);
            vcam.SetActive(true);
            //audioListener.SetActive(true);
            playerCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            playerCamera.SetActive(false);
            //Weapon.SetActive(false);
            vcam.SetActive(false);
            //audioListener.SetActive(false);
            playerCamera.GetComponent<AudioListener>().enabled = false;
            AssignRemoteLayer();
        }

        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        PlayerController _player = GetComponent<PlayerController>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void OnDisable()
    {
        GameManager.UnRegisterPlayer(transform.name);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }
}
