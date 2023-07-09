using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ReachObjective : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Object.HasStateAuthority == false) return;
        
        var col = other.gameObject.GetComponent<PlayerTest>();

        if (col != null)
        {
                RPC_EndGame();
        }
 
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_EndGame()
    {
        var a = FindObjectOfType<GameStateHandeler>();
        a.RPC_WinGame();
    }
}