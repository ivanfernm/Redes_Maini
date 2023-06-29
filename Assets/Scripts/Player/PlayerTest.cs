using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    [SerializeField] private NetworkCharacterControllerPrototype _cc;
    [SerializeField] private Camera _camera;
    [SerializeField] private NetworkBullet _bullet;
    private Vector3 _forward;
    [Networked] private TickTimer _delay { get; set;}
    
    public float speed = 5f;
    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    public override void Spawned()
    {
        _camera = Camera.main;
        _camera.GetComponent<ThirdPersonCamera>()._targer = gameObject.transform;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(5 *data.direction  * ( speed * Runner.DeltaTime));
           
            if (data.direction.sqrMagnitude > 0) _forward = data.direction;

            if (_delay.ExpiredOrNotRunning(Runner))
            {
                if ((data.buttons & NetworkInputData.MOUSEBUTTON) != 0)
                {
                    _delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_bullet, transform.position + _forward, Quaternion.LookRotation(_forward),
                        Object.InputAuthority, (Runner, o) => { o.GetComponent<NetworkBullet>().Init(); });
                }
                
            }
         
        }
    }
}
