using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateHandeler : NetworkBehaviour
{
    public static GameStateHandeler Instance;

    public enum GameState
    {
        Starting,
        Running,
        Ending
    }

    public enum GameResult
    {
        Win,
        Lose
    }

    public int activePlayers;

    [SerializeField] private float _PlayersToStart = 2;
    [Networked] private GameState _gameState { get; set; }

    [SerializeField] private Text StateText;
    [SerializeField] private MainCanvasHandeler CanvasHandeler;
    [SerializeField] private GameObject _WaitingPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Instance = this;

        CanvasHandeler = GetComponentInChildren<MainCanvasHandeler>();

    }

    public GameState GetGameState()
    {
        return _gameState;
    }

    public override void FixedUpdateNetwork()
    {
        activePlayers = Runner.ActivePlayers.Count();

        if (_gameState != GameState.Running)
        {
            if (activePlayers >= _PlayersToStart)
            {
                _gameState = GameState.Running;
                TimeObjective.Instance.StartTimer();
            }
        }
        else
        {
            if (activePlayers < _PlayersToStart)
            {
                _gameState = GameState.Starting;
            }
        }

        switch (_gameState)
        {
            case GameState.Starting:
                _WaitingPanel.gameObject.SetActive(true);
                StateText.text = "Starting";
                StateText.color = Color.yellow;
                break;
            case GameState.Running:
                _WaitingPanel.gameObject.SetActive(false);
                StateText.text = "Running";
                StateText.color = Color.green;
                break;
            case GameState.Ending:
                _WaitingPanel.gameObject.SetActive(false);

                StateText.text = "Ending";
                StateText.color = Color.red;
                break;
        }
    }

    public override void Spawned()
    {
        // Instance = this;
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

    public void Ending(GameResult result)
    {
        StartCoroutine(GameEnding(result));
    }

    public IEnumerator GameEnding(GameResult result)
    {
        _gameState = GameState.Ending;
        if (result == GameResult.Win)
        {
            CanvasHandeler.ShowWinPanel();
        }
        else
        {
            CanvasHandeler.ShowLosePanel();
        }
        yield return new WaitForSeconds(5f);
    }

    public void PlayerJoint()
    {
        if (activePlayers >= _PlayersToStart)
        {
            Starting();
        }
    }
}