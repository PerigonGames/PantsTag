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
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = m_gameVersion;
            }
        }

        #region MonoBehaviourPunCallbacks

        /// <summary>
        /// If we connect succesfully then we fire this
        /// </summary>
        public override void OnConnectedToMaster()
        {
            // Cool we connected to master, so let's join a random room. 
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        }

        /// <summary>
        /// If connection didn't work then oh no we fire this 
        /// </summary>
        /// <param name="cause"></param>
        public override void OnDisconnected(DisconnectCause cause)
        {
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
            Debug.Log("PUN basics tutorial?laucnher: OnjoinedRoom() called by PUN, now this client is in a room woot woot ");
        }

        #endregion


        #endregion

    }
}
