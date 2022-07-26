using UnityEngine;
using Photon.Pun;

public class PlayerNetwork : MonoBehaviourPunCallbacks
{
    public GameObject Local_XRRig_GO;
    public GameObject Avatar_Head_GO;
    public GameObject Avatar_Body_GO;
    
    void Start()
    {
        if (photonView.IsMine)
        {
            //The Player is Local
            Local_XRRig_GO.SetActive(true);
            SetLayerRecursively(Avatar_Head_GO, 6);
            SetLayerRecursively(Avatar_Body_GO, 7);

        }
        else
        {
            //The Player is Remote Player
            Local_XRRig_GO.SetActive(false);
            SetLayerRecursively(Avatar_Head_GO, 0);
            SetLayerRecursively(Avatar_Body_GO, 0);
        }
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
