using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject connectOptionsPanel;
    public GameObject connectWithNamePanel;


    #region Unity Methods

    private void Start()
    {
        connectOptionsPanel.SetActive(true);
        connectWithNamePanel.SetActive(false);   
    }

    #endregion
}
