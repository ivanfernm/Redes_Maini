using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ReachObjective : NetworkBehaviour
{
    [SerializeField] private GameObject ObjectToReach;

    [SerializeField] TimeObjective _timeObjective;

    [SerializeField] private MainCanvasHandeler CanvasHandeler;

    public void NotifyObjectiveReached()
    {
        _timeObjective.StopTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        var col = other.gameObject.GetComponent<PlayerTest>();

        if (col != null)
        {
            NotifyObjectiveReached();
            CanvasHandeler.ShowWinPanel();
        }
 
    }
}