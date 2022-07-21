using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;
    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        
    }

    public void OnEnteredButtonClicked_Outdoor()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        Hashtable expectedRoomProperties = new Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 0);
    }

    public void OnEnteredButtonClicked_PlayArea()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_PLAYAREA;
        Hashtable expectedRoomProperties = new Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType } };
        PhotonNetwork.JoinRandomRoom(expectedRoomProperties, 0);

    }

    #endregion

    #region PhotonCallback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with name : " + PhotonNetwork.CurrentRoom.Name);
    }



    public override void OnJoinedRoom()
    {
        Debug.Log("The Local Player : " + PhotonNetwork.NickName + " Joined to " + PhotonNetwork.CurrentRoom.Name + " Player Count : " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with the map : " + (string)mapType);
            }
        }

    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "Joined to : " + PhotonNetwork.CurrentRoom.Name + " PlayerCount : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string roomName = "roomNumber_" + Random.Range(0,100);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;


        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        //we have 2 different maps
        //1. Outdoor = "outdoor"
        //2. School = "school"

        Hashtable customRoomProperties = new Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    #endregion
}
