using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON = 0x01;
    
    public byte buttons;
    public Vector3 direction;
}
