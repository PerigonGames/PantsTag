using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace PerigonGames
{
    public class Launcher : MonoBehaviourPunCallbacks
    {

        #region Private Serializable Fields

        [SerializeField]
        private byte m_maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allow syou to make breaking changes).
        /// </summary>
        private string m_gameVersion = "1";

        private bool m_isConnecting = false;

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
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }


        #endregion

        #region Public Methods

        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                m_isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = m_gameVersion;
            }
        }

        [Tooltip("The UI Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject controlPanel = null;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel = null;


        #region MonoBehaviourPunCallbacks

        /// <summary>
        /// If we connect succesfully then we fire this
        /// </summary>
        public override void OnConnectedToMaster()
        {
            // Cool we connected to master, so let's join a random room. 
            
            if (m_isConnecting)
            {
                m_isConnecting = false;
                PhotonNetwork.JoinRandomRoom();
            }
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        /// <summary>
        /// If connection didn't work then oh no we fire this 
        /// </summary>
        /// <param name="cause"></param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            m_isConnecting = false;
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomRoomFailed() was called by PUN, no random room available");

            // If we fail to join any room then we create our own new room
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = m_maxPlayersPerRoom});
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("RoomFor" + PhotonNetwork.CurrentRoom.PlayerCount);
            /*if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the RoomFor1");

                PhotonNetwork.LoadLevel("RoomFor" + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                Debug.Log("We load the RoomFor1");

                PhotonNetwork.LoadLevel("RoomFor2");
            }*/
        }

        #endregion


        #endregion

    }
}
