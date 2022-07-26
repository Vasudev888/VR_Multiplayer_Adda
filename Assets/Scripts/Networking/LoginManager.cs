using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField userInputField;
    public GameObject connectButton;

    #region Unity Methdods

    #endregion


    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        PhotonNetwork.NickName = userInputField.text;
        PhotonNetwork.ConnectUsingSettings();

    }

    #endregion

    #region PhotonCallback Methods


    public override void OnConnected()
    {
        Debug.Log("OnConnected is called, The server is available");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server with playerName : " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel(1);
    }

    #endregion


    #region Custom Info Methods

    public void EnableConnectButton()
    {
        if (userInputField.text.Length >= 4)
        {
            connectButton.SetActive(true);
        }
        else
        {
            connectButton.SetActive(false);
        }
    }

    #endregion

}
