using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviour
{
    #region Private Serializable Fields


    #endregion

    #region Private Fields

    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allow syou to make breaking changes).
    /// </summary>
    private string gameVersion = "1";

    #endregion

    #region MonoBehaviour CallBacks

    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master and all clients in the same room sync their level automatically 
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        Connect();
    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        // Are we conencted?
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }


    #endregion

}
