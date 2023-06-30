using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private bool _mouseButton0;
    private NetworkRunner _runner;

    [SerializeField] private NetworkPrefabRef _stateHandeler;
    [SerializeField] private GameStateHandeler StateHandeler;
  
    [SerializeField] private List<Transform> PlayersSpawnPoints;


    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private void Update()
    {
        _mouseButton0 = _mouseButton0 | Input.GetMouseButton(0);
        
    }

    /*private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200 * 3, 40 * 3), "Host"))
            {
                StartGame(GameMode.Host);
            }

            if (GUI.Button(new Rect(0, 100, 200 * 3, 40 * 3), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }*/

    /*async void StartGame(GameMode mode)
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        var playerCount = _runner.SessionInfo.PlayerCount;

        if (playerCount < 2)
        {
            //TODO - PANEL THAT SAID WAITING FOR PLAYERS
            Debug.Log("Waiting for players");
        }
        else
        {
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "TestRoom",
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }
        // Start or join (depends on gamemode) a session with a specific name
    }*/

    //TODO - Check the spawn points for the players
    List<(PlayerRef player, NetworkRunner runner)> playerRunners = new List<(PlayerRef player, NetworkRunner runner)>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)

    {
        //create a collection of tuples whit the player and the runners using linq


        playerRunners.Add((player, runner));
        Debug.Log(playerRunners.Count);

        
        if (runner.IsServer)
        {
            // Create a unique position for the player
            var playerIndex = Random.Range(0, PlayersSpawnPoints.Count);
            Vector3 spawnPosition = PlayersSpawnPoints[playerIndex].position;
            //new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            NetworkObject networkPlayerObject =
                runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
            //Remove player index from the list
            PlayersSpawnPoints.RemoveAt(playerIndex);
                    
            var stateHandeler = runner.Spawn(_stateHandeler, Vector3.zero, Quaternion.identity, player);
            StateHandeler = stateHandeler.GetComponent<GameStateHandeler>();
        }
        else
        {
            Debug.Log("Joining as client");
        }

        if (playerRunners.Count >= 2)
        {
            StateHandeler.Starting();
        }
  
        
        
        // cuando se une el player debugear esperando por player  y una vez que haya mas de 1 se spawnean

        /*
        if (runner.IsServer)
        {
            // Create a unique position for the player
            var playerIndex = Random.Range(0, PlayersSpawnPoints.Count);
            Vector3 spawnPosition = PlayersSpawnPoints[playerIndex].position;
            //new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars so we can remove it when they disconnect
            _spawnedCharacters.Add(player, networkPlayerObject);
            //Remove player index from the list
            PlayersSpawnPoints.RemoveAt(playerIndex);
        }
        else
        {
            Debug.Log("Joining as client");
        }*/
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if (Input.GetKey(KeyCode.W))
            data.direction += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            data.direction += Vector3.back;

        if (Input.GetKey(KeyCode.A))
            data.direction += Vector3.left;

        if (Input.GetKey(KeyCode.D))
            data.direction += Vector3.right;

        if (_mouseButton0)
        {
            data.buttons |= NetworkInputData.MOUSEBUTTON;
            _mouseButton0 = false;
        }

        input.Set(data);
    }

    #region CallBacks

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    #endregion
}