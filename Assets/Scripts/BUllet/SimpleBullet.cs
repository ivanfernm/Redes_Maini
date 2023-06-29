using System;
using System.Collections;
using Fusion;
using UnityEditor.UIElements;
using UnityEngine;

public class SimpleBullet : NetworkBehaviour
{
    [SerializeField] private float simpleBullterDmg;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _lifeTime = 5;
    private float _timer;

    public override void Spawned()
    {
        _timer = 0;
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += transform.forward * _speed * Runner.DeltaTime;
        
        _timer += Runner.DeltaTime;
        if (_timer >= _lifeTime)
        {
            Runner.Despawn(Object);
        }
    }
    
    /*

    private void OnTriggerEnter(Collider other)
    {
        if (!Object || !Object.HasStateAuthority) return;

        if (other.TryGetComponent(out PlayerBasics otherPlayer)) {
          otherPlayer.TakeDamage(simpleBullterDmg);
      }
      Runner.Despawn(Object);
    }
    */
    
}
