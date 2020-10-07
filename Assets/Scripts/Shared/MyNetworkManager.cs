using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class MyNetworkManager : NetworkManager
{
    public Transform ctSpawn;
    public Transform tSpawn;
    GameObject Hostage;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? ctSpawn : tSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball if two players
        if (numPlayers == 2)
        {
            Hostage = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Hostage"));
            NetworkServer.Spawn(Hostage);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (Hostage != null)
            NetworkServer.Destroy(Hostage);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
