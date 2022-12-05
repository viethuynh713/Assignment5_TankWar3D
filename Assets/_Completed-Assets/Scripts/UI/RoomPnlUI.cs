
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPnlUI : MonoBehaviour
{
    [SerializeField]
    RoomPnlControl m_control;


    public void AddBotA()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_control.AddBot(Team.A);
        }
    }

    public void AddBotB()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            m_control.AddBot(Team.B);
        }
    }

    public void JoinTeamA()
    {
        if(m_control.teamA.Count<4 && m_control.teamB.ContainsKey(PhotonNetwork.LocalPlayer.UserId))
        {
            m_control.RemovePlayerUI(PhotonNetwork.LocalPlayer.UserId,Team.B);
            m_control.JoinTeam(Team.A,PhotonNetwork.LocalPlayer.UserId,PhotonNetwork.LocalPlayer.NickName);
        }
    }

    public void JoinTeamB()
    {
          if(m_control.teamB.Count<4&& m_control.teamA.ContainsKey(PhotonNetwork.LocalPlayer.UserId))
        {
            m_control.RemovePlayerUI(PhotonNetwork.LocalPlayer.UserId,Team.A);
            m_control.JoinTeam(Team.B,PhotonNetwork.LocalPlayer.UserId,PhotonNetwork.LocalPlayer.NickName);
        }
    
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadSceneAsync("Loading");
    }
    public void PlayBtn()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            m_control.PlayGame();
        }
    }
}
