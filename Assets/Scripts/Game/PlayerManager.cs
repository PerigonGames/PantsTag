using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PerigonGames
{
    public class PlayerManager : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        [SerializeField]
        private PhotonView photonView;
        public float speed = 10.0f;
        public float rotationSpeed = 100.0f;

        private void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("Missing camera component on playerprefab");
            }
        }

        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) return;
            PlayerMovement();
         
        }

        private void PlayerMovement()
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;


            transform.Translate(0, 0, translation);

            transform.Rotate(0, rotation, 0);
        }

    }
}