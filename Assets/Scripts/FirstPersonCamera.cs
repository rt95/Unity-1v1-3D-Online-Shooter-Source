using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirstPersonCamera : MonoBehaviour
{

    Player localPlayer;

    void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += Instance_OnLocalPlayerJoined;
    }

    private void Instance_OnLocalPlayerJoined(Player obj)
    {
        localPlayer = obj;
    }

    void Update() {
        
    }
}
