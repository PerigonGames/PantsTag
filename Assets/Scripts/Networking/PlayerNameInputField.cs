using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace PerigonGames
{
    /// <summary>
    /// Player name input field. User can input their own name. 
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants

        const string playerNamePrefKey = "PlayerName";

        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {

            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets name of the player, save in playerprefs
        /// </summary>
        /// <param name="value"> name of player </param>
        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey,value);
        }

        #endregion
    }
}
