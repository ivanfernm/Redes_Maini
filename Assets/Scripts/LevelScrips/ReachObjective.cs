using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ReachObjective : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var col = other.gameObject.GetComponent<PlayerTest>();

        if (col != null)
        {
            if (col.Object.HasInputAuthority)
            {
               GameStateHandeler.Instance.Ending(GameStateHandeler.GameResult.Win);
            }
        }
 
    }
}