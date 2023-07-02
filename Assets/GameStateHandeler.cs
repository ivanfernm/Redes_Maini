using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateHandeler : NetworkBehaviour
{

    public static GameStateHandeler Instance { get; private set; }

    enum GameState
    {
        Starting,
        Running,
        Ending
    }

    public int activePlayers;

    [SerializeField] private float _gameSessionLenth = 180.0f;

    [SerializeField] private Text _inGameTimerDisplay = null;

    [SerializeField] private float _PlayersToStart = 2;

    [Networked] private TickTimer _timer { get; set; }

    [Networked] private GameState _gameState { get; set; }

    [SerializeField] private Text StateText;
    [SerializeField] private MainCanvasHandeler CanvasHandeler;

    public override void Spawned()
    {
        Instance = this;

        CanvasHandeler = GetComponentInChildren<MainCanvasHandeler>();
        CanvasHandeler._RestartBTN.onClick.AddListener(Ending);
        CanvasHandeler._ExitBTN.onClick.AddListener(Ending);
    }

    private void FixedUpdate()
    {
        activePlayers = Runner.ActivePlayers.Count();
    }

    public override void FixedUpdateNetwork()
    {
        switch (_gameState)
        {
            case GameState.Starting:
                StateText.text = "Starting";
                StateText.color = Color.yellow;
                break;
            case GameState.Running:
                StateText.text = "Running";
                StateText.color = Color.green;
                break;
            case GameState.Ending:
                StateText.text = "Ending";
                StateText.color = Color.red;
                break;
        }
    }

    public void Starting()
    {
        StartCoroutine(GameStarting());
    }

    public IEnumerator GameStarting()
    {
        _gameState = GameState.Starting;
        Debug.Log(" Game Starting");
        yield return new WaitForSeconds(5f);
        _gameState = GameState.Running;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Ending()
    {
        StartCoroutine(GameEnding());
    }

    public IEnumerator GameEnding()
    {
        _gameState = GameState.Ending;
        yield return new WaitForSeconds(5f);
        //Change scene to Main Menu
        //SceneManager.LoadScene("Main Menu");
    }

    public void PlayerJoint()
    {
        if (activePlayers >= _PlayersToStart)
        {
            Starting();
        }
    }


}