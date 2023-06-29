using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkBullet : NetworkBehaviour
{
    [SerializeField] private float _duration;
    [Networked] private  TickTimer _life { get; set;}

    public void Init()
    {
        _life = TickTimer.CreateFromSeconds(Runner, _duration);
    }
    
    public override void FixedUpdateNetwork()
    {
        if(_life.Expired(Runner))Runner.Despawn(Object);
        else
        {
            transform.position += transform.forward * (5 * Runner.DeltaTime);
        }
        
    }
}
