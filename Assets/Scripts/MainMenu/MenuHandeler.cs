using System;
using MainMenu;
using UnityEngine;
using UnityEngine.UI;


public class MenuHandeler : MonoBehaviour 
{

    [SerializeField] NetworkRunnerHandeler _networkHandeler;
    
    [Header("Panels")]
    [SerializeField] GameObject _initialPanel;
    [SerializeField] SessionListHandeler _sessionListHandeler;
    [SerializeField] GameObject _hostGamePanel;
    [SerializeField] GameObject _statusPanel;
    
    [Header("Buttons")]
    [SerializeField] Button _joinLobbyBTN;
    [SerializeField] Button _openHostPanelBTN;
    [SerializeField] Button _hostGameBTN;
    
    [Header("Inputfields")]
    [SerializeField] InputField _hostSessionName;
    
    [Header("Texts")]
    [SerializeField] Text _statusText;

    private void Start()
    {
        //A cada boton que tenemos le agregamos por codigo el metodo que deberian ejecutar cuando son clickeados

        _joinLobbyBTN.onClick.AddListener(BTN_JoinLobby);

        _openHostPanelBTN.onClick.AddListener(BTN_ShowHostPanel);

        _hostGameBTN.onClick.AddListener(BTN_CreateGameSession);

        //Cuando el Network Runner se termine de conectar a un Lobby
        //Le decimos mediante la suscripcion al evento que apague el Panel de Estado y prenda el Browser

        _networkHandeler.OnJoineddLobby += () =>
        {
            _statusPanel.SetActive(false);
            _sessionListHandeler.gameObject.SetActive(true);
        };    }

    #region Buttons M
    
    //Cuando se clickea en el boton de entrar a un lobby:
    void BTN_JoinLobby()
    {
        _networkHandeler.JoinLobby(); 
        _initialPanel.SetActive(false);
        _statusPanel.SetActive(true);
        
        _statusText.text = "Connecting to Lobby...";
    }
    
    //Cuando se clickea en el boton de mostrar el menu para crear una sala:
    void BTN_ShowHostPanel()
    {
        //Apagamos el Browser de sesiones
        _sessionListHandeler.gameObject.SetActive(false);

        //Prendemos el panel necesario
        _hostGamePanel.SetActive(true);
    }

    //Cuando se clickea en el boton de crear una sala
    void BTN_CreateGameSession()
    {
        //Le pedimos al network handler que cree la sesion en la que vamos a ser Host
        _networkHandeler.CreateSession(_hostSessionName.text, "Game");
    }

    #endregion
}