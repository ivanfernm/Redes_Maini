using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle_Door : MonoBehaviour
{
    [SerializeField] private bool _isLocked = true;
    
    public void Unlock()
    {
        
        StartCoroutine("UnlockDoor");
    }
    
    IEnumerator UnlockDoor()
    {
        var position = transform.position;
        _isLocked = false;
        transform.position = Vector3.zero;
        yield return new WaitForSeconds(5.0f);
        _isLocked = true;
        transform.position = position;
    }
}
