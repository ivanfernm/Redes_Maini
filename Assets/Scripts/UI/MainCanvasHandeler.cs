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
    
   public static MainCanvasHandeler Instance { get; private set; }

    [Header("Buttons")] [SerializeField] public Button _RestartBTN;
    [SerializeField] public Button _ExitBTN;

    public override void Spawned()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
          Instance = this;
        }
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