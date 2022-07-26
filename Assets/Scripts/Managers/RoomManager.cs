using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Collections.Generic;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;
    private int maxPlayers = 10;

    public TextMeshProUGUI occupancyRateText_PlayArea;
    public TextMeshProUGUI occupancyRateText_Outdoor;


    #region UnityMethods 

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    #endregion

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
                if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_PLAYAREA)
                {
                    //Load PlayArea Scene
                    PhotonNetwork.LoadLevel(2);
                }
                else if ((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    //Load Outdoor scene
                    PhotonNetwork.LoadLevel(3);
                }
            }
        }

    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to : " + PhotonNetwork.CurrentRoom.Name + " PlayerCount : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            //There is no room at all
            occupancyRateText_PlayArea.text = 0 + " / " + maxPlayers;
            occupancyRateText_Outdoor.text = 0 + " / " + maxPlayers;
        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                //Update the Outdoor room occupancy
                Debug.Log("Room is Outdoor. Player count is : " + room.PlayerCount);
                occupancyRateText_Outdoor.text = room.PlayerCount + " / " + maxPlayers;
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_PLAYAREA))
            {
                Debug.Log("Room is PlayArea. Player count is : " + room.PlayerCount);
                occupancyRateText_PlayArea.text = room.PlayerCount + " / " + maxPlayers;
            }
        }
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby");
    }
    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        string roomName = "roomNumber_" + mapType + Random.Range(0,100);
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
