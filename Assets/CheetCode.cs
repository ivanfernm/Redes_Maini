using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheetCode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            var a = FindObjectOfType<ReachObjective>();
            transform.position = a.transform.position;
        }
    }
}
