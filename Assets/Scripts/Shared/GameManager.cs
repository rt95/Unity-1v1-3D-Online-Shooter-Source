using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    public MatchSettings matchSettings;

    #region Player tracking

    private static Dictionary<string, PlayerController> players = new Dictionary<string, PlayerController>();

    public static void RegisterPlayer(string _netID, PlayerController _player)
    {
        string _playerID = "Player " + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    { 
        players.Remove(_playerID);
    }

    public static PlayerController GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    #endregion

    public event System.Action<Player> OnLocalPlayerJoined;

    private GameObject gameObject;

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new GameManager();
                m_Instance.gameObject = new GameObject("_gameManager");
                m_Instance.gameObject.AddComponent<InputController>();
            }
            return m_Instance;
        }
    }

    private InputController m_inputController;
    public InputController InputController
    {
        get
        {
            if (m_inputController == null)
                m_inputController = gameObject.GetComponent<InputController>();
            return m_inputController;
        }
    }

    private Player m_localPlayer;
    public Player LocalPlayer
    {
        get
        {
            return m_localPlayer;
        }
        set
        {
            m_localPlayer = value;
            if (OnLocalPlayerJoined != null)
                OnLocalPlayerJoined(m_localPlayer);
        }
    }

    public void Win(string playerName)
    {
        Debug.Log(playerName + " Lost");

        

        //if (isLocalplayer && isAlive)
        //{
        //    //player code stuff
        //    Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        //}
        //Display a win message

            //if (gameobject.name.Equals(“winner”))

            //if (GameObject.FindGameObjectsWithTag("Player").Length == 1) {

            //};

    }

}
