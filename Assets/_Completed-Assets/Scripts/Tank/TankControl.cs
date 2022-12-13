using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
namespace Complete
{
    public class TankControl : MonoBehaviour
    {
        public CinemachineVirtualCamera cam;
        public bool IsBot = false;
        public Team team;
        public PhotonView view;
        public void SetCamera() {
            if (CanMove())
            {
                cam.Priority = 99;
            }
        }
        public bool CanMove()
        {
            if(view.IsMine && !IsBot) return true;

            return false;
        }
        private void OnCollisionEnter(Collision other) 
        {
            if(other.gameObject.CompareTag("Health"))
            {
            
                GetComponent<TankHealth>().TakeDamage(-30);
                Destroy(other.gameObject);
            }
            if(other.gameObject.CompareTag("Speed"))
            {
                GetComponent<TankMovement>().SetMaxSpeed(20);
                Destroy(other.gameObject);
            }
        }
        public void SetBot()
        {
            IsBot = true;
            gameObject.AddComponent<TankBT>();
        }
    }
}
