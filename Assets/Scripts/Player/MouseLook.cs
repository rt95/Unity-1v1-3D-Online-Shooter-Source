using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;

    public PlayerAim playerAim;

    public Transform playerBody;

    [SerializeField] Transform LookAtObject;

    float xRotation = 0f;

    Quaternion iniRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        iniRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
            LookDirection();
    }

    void LookDirection()
    {
        float mouseX = GameManager.Instance.InputController.MouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = GameManager.Instance.InputController.MouseInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        playerAim.SetRotation(mouseY);
        transform.LookAt(LookAtObject);
    }

    void LateUpdate()
    {
        transform.localRotation = iniRot;
    }


}
