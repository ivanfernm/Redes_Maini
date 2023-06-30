using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTest : NetworkBehaviour
{
    [SerializeField] private NetworkCharacterControllerPrototype _cc;
    [SerializeField] private GameObject _camera;
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

        if (Camera.main != null)
        {
            _camera = Camera.main.gameObject;
            _camera.GetComponent<ThirdPersonCamera>()._targer = gameObject.transform;
            
        }
        else
        {
            var cam= Instantiate(_camera, transform.position, quaternion.identity);
            _camera = cam;
            cam.GetComponent<ThirdPersonCamera>()._targer = gameObject.transform;
        }
        //_camera = Camera.main;
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
