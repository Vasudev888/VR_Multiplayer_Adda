using UnityEngine;
using UnityEngine.UI;


public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameObject GoHomeButton;

    private void Start()
    {
        GoHomeButton.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndLoadScene);
    }
}
