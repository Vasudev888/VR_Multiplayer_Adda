using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject genericVRPlayerPrefab;
    [SerializeField] Vector3 spawnPos;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(genericVRPlayerPrefab.name, spawnPos, Quaternion.identity);
        }
    }

}
