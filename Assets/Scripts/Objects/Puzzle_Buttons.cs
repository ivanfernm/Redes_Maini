using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Buttons : MonoBehaviour
{
    [SerializeField] private GameObject _door;

    private void OnTriggerStay(Collider other)
    {
        var col = other.gameObject.GetComponent<PlayerTest>();
        if (col != null)
        {
            _door.GetComponent<Puzzle_Door>().Unlock();
        }
    }
}