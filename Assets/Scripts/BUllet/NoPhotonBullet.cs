using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPhotonBullet : MonoBehaviour
{
    [SerializeField] private float dmg;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _lifeTime = 5;
    private float _timer;

    private void Start()
    {
        _timer = 0;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
        _timer += Time.fixedDeltaTime;
        if(_timer >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
