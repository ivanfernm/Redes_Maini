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


    [SerializeField] private float _timeToWin;
    [SerializeField] private float _TimePass;

    [SerializeField] public MainCanvasHandeler _menuHandeler;

    private void Start()
    {
    }

    private void Update()
    {
        if (_TimePass >= _timeToWin)
        {
            SessionLost();
        }
    }

    void SessionLost()
    {
        _menuHandeler.ShowLosePanel();  
    }

    public void StopTimer()
    {
    }
}