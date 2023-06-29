using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using OpenCover.Framework.Model;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandeler : MonoBehaviour,INetworkRunnerCallbacks
{
    [Header("Network Runner Prefab")]
    [SerializeField] NetworkRunner _runnerPrefab;
    NetworkRunner CurrentRunner;

    //Se va a llamar cuando la conexion al lobby sea exitosa
    public event Action OnJoineddLobby;

    //Se va a llamar cuando la lista de sesiones se actualice
    public event Action<List<SessionInfo>> OnSessionListUpdate; 

    private void Start()
    {
        //JoinLobby();
    }

    #region Host/Join Session
    public void CreateSession(string sessionName, string sceneName)
    {
        //Creamos una sesion en el Network Runner actual con el nombre de la sesion y el nombre de la escena
        // Client ==> Host
        var clientTask = InitiializeSession(CurrentRunner,
            GameMode.Host,sessionName,
            SceneUtility.GetBuildIndexByScenePath($"Scenes/{sceneName}"));
    }
    
    public void JoinSession(SessionInfo sessionInfo)
    {
        //Nos unimos a la sesion con el nombre de la sesion y el nombre de la escena
        var clientTask = InitiializeSession(CurrentRunner,GameMode.Client,sessionInfo.Name,
            SceneManager.GetActiveScene().buildIndex);
    }
    
    async Task InitiializeSession(NetworkRunner runner,GameMode gameMode, string sessionName,SceneRef scene)
    {
        
        var sceneManager = runner.GetComponent<NetworkSceneManagerDefault>();
        //TODO - Check if theres 2 players conected to the session before starting the game
        var result = await runner.StartGame(new StartGameArgs
        {
            PlayerCount = 2,
            GameMode = gameMode,
            Scene = scene,
            SessionName = sessionName,
            CustomLobbyName = "Normal Lobby",
            SceneManager = sceneManager
        });

       if (!result.Ok)
       {
           Debug.LogError("[Custom Error] Unable to Start Game");
       }
       else
       {
           Debug.Log("[Custom Msg] Game Started");
       }
    }
    #endregion

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        OnSessionListUpdate?.Invoke(sessionList);
        /*//Si existe una session
        if (sessionList.Count > 0)
        {
            //preguntamos si la cantidad de jugadores es menor a la cantidad maxima de jugadores que pueden conectarse
            foreach (var sessionInfo in sessionList)
            {
                //Si es menor a la cantidad maxima de jugadores que pueden conectarse
                if (sessionInfo.PlayerCount < sessionInfo.MaxPlayers)
                {
                    JoinSession(sessionInfo);
                    
                    return;
                }
            }
        }
        
        //Si no existe una session creamos la sala*/
        //CreateSession("Lobby","Game");
    }
    
#region Lobby
    public void JoinLobby()
    {
        if(CurrentRunner) Destroy(CurrentRunner.gameObject);
        
        CurrentRunner = Instantiate(_runnerPrefab);
        
        //Agrega los callbacks al prefab
        CurrentRunner.AddCallbacks(this);

        var clientTask = JointLobbyTask();
    }

    async Task JointLobbyTask()
    {
        var result = await  CurrentRunner.JoinSessionLobby(SessionLobby.Custom,("Lobby"));

        if (!result.Ok)
        {
            Debug.LogError("[Custom Error] Unable to Join Lobby");
        }
        else
        {
            Debug.Log("Joined Lobby");
            OnJoineddLobby?.Invoke();
        }
    }
    
#endregion   
#region Conections
     public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

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
