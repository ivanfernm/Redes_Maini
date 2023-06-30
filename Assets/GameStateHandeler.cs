using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateHandeler : NetworkBehaviour
{
    enum GameState
    {
        Starting,
        Running,
        Ending
    }
    
    [SerializeField] private float _gameSessionLenth = 180.0f;
    
    [SerializeField] private Text _inGameTimerDisplay = null;
    
    [Networked] private TickTimer _timer { get; set; }
    
    [Networked] private GameState _gameState { get; set; }

    [SerializeField] private Text StateText;
    [SerializeField] private MainCanvasHandeler CanvasHandeler;

    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
        
        _gameState = GameState.Starting;
        _timer = TickTimer.CreateFromSeconds(Runner, _gameSessionLenth);
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

    public IEnumerator GameStarting()
    {
        _gameState = GameState.Starting;
        yield return new WaitForSeconds(5);
        _gameState = GameState.Running;
    }
    
    public IEnumerator GameEnding()
    {
        _gameState = GameState.Ending;
        yield return new WaitForSeconds(5);
        //Change scene to Main Menu
        SceneManager.LoadScene("Main Menu");
    }
}
