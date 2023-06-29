using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
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
    
    
    public override void Spawned()
    {
        if (Object.HasStateAuthority == false) return;
        
        _gameState = GameState.Starting;
        _timer = TickTimer.CreateFromSeconds(Runner, _gameSessionLenth);
    }
}
