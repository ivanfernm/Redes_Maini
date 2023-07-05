using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEditor;
using UnityEngine;

public class TimeObjective : NetworkBehaviour
{
    //This script is going to be spawned when the game starts and is it have the responsability of create a timer when the game starts
    //and when the timer ends, should send a rpc to all the players to show the lose panel
    //and the win panel to the player who is the last one standing.

    //Must have the canvas with the panels and the buttons to restart and exit the game.

    public static TimeObjective Instance;

    [Networked] public float _timeToWin { get; set; }
    [Networked] private float _TimePass {get; set; }
    
   //[Networked] public TickTimer _timer { get; set; }

    [SerializeField] public MainCanvasHandeler _menuHandeler;

    private void Awake()
    {
        Instance = this;
    }
    public void StartTimer()
    {
       _TimePass = 0;
    }

    public override void FixedUpdateNetwork()
    {
        if (GameStateHandeler.Instance.GetGameState() == GameStateHandeler.GameState.Running)
        {
            var a  = _TimePass += Runner.DeltaTime;

            _timeToWin = _timeToWin - a;
            
            if ( _timeToWin <= 0)
            {
                
                GameStateHandeler.Instance.Ending(GameStateHandeler.GameResult.Lose);
                
            }
            
        }
        else
        {
            return;
        }
    }
    
}