using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MainCanvasHandeler : NetworkBehaviour
{
    [Header("Panels")] [SerializeField] private GameObject _WinPannel;
    [SerializeField] private GameObject _LosePannel;

    [Header("Buttons")] [SerializeField] private Button _RestartBTN;
    [SerializeField] private Button _ExitBTN;

    void Start()
    {
        
    }

    public void ShowWinPanel()
    {
        _WinPannel.SetActive(true);
    }
    
    public void ShowLosePanel()
    {
        _LosePannel.SetActive(true);
    }
    
}