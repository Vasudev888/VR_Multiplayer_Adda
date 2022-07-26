using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " Joined to : " + PhotonNetwork.CurrentRoom.Name + " PlayerCount : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
}
