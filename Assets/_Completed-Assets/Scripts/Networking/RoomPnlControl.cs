using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
namespace Complete
{
public class RoomPnlControl : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerTemplate[] m_ListTeamA;

    [SerializeField]
    private PlayerTemplate[] m_ListTeamB;

    public PhotonView view;

    public Dictionary<string, TankInfos> teamA;

    public Dictionary<string, TankInfos> teamB;

    private void Start()
    {
        teamA = new Dictionary<string, TankInfos>();
        teamB = new Dictionary<string, TankInfos>();
        if (PhotonNetwork.IsMasterClient)
        {
            JoinTeam(Team.A,
            PhotonNetwork.LocalPlayer.UserId,
            PhotonNetwork.LocalPlayer.NickName);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Debug.Log();
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var tank in teamA)
            {
                if (tank.Value.IsBot)
                {
                    view.RPC("AddBotTeamA", newPlayer);
                }
                else
                {
                    view
                        .RPC("JoinTeamA",
                        newPlayer,
                        tank.Key,
                        tank.Value.PlayerName);
                }
            }
            foreach (var tank in teamB)
            {
                if (tank.Value.IsBot)
                {
                    view.RPC("AddBotTeamB", newPlayer);
                }
                else
                {
                    view
                        .RPC("JoinTeamB",
                        newPlayer,
                        tank.Key,
                        tank.Value.PlayerName);
                }
            }
            if (teamA.Count < 4)
            {
                JoinTeam(Team.A, newPlayer.UserId, newPlayer.NickName);
            }
            else if (teamB.Count < 4)
            {
                JoinTeam(Team.B, newPlayer.UserId, newPlayer.NickName);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemovePlayer(otherPlayer.UserId);
    }

    [PunRPC]
    public void AddBotTeamA()
    {
        if (teamA.Count < 4)
        {
            var bot =
                new TankInfos("Bot " + Random.Range(10, 100), true, Team.A);
            var botID = Random.Range(100000000, 999999999).ToString();
            teamA.Add (botID, bot);
            foreach (var item in m_ListTeamA)
            {
                // Debug.Log (item);
                if (item.gameObject.active == false)
                {
                    item.DisplayUI(bot.PlayerName, botID);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    [PunRPC]
    public void AddBotTeamB()
    {
        if (teamA.Count < 4)
        {
            var bot =
                new TankInfos("Bot " + Random.Range(10, 100), true, Team.B);
            var botID = Random.Range(100000000, 999999999).ToString();
            teamB.Add (botID, bot);
            foreach (var item in m_ListTeamB)
            {
                if (item.gameObject.active == false)
                {
                    item.DisplayUI(bot.PlayerName, botID);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    [PunRPC]
    public void JoinTeamB(string id, string playerName)
    {
        if (teamB.Count < 4)
        {
            var newPlayer = new TankInfos(playerName, false, Team.B);
            if (PhotonNetwork.LocalPlayer.UserId == id)
            {
                GameManager.Instance.myTankInfo = newPlayer;
            }
            teamB.Add (id, newPlayer);
            foreach (var item in m_ListTeamB)
            {
                if (item.gameObject.active == false)
                {
                    item.DisplayUI(newPlayer.PlayerName, id);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    [PunRPC]
    public void JoinTeamA(string id, string playerName)
    {
        if (teamB.Count < 4)
        {
            var newPlayer = new TankInfos(playerName, false, Team.B);
            if (PhotonNetwork.LocalPlayer.UserId == id)
            {
                GameManager.Instance.myTankInfo = newPlayer;
            }
            teamA.Add (id, newPlayer);
            foreach (var item in m_ListTeamA)
            {
                if (item.gameObject.active == false)
                {
                    item.DisplayUI(newPlayer.PlayerName, id);
                    item.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public void JoinTeam(Team team, string id, string playerName)
    {
        switch (team)
        {
            case Team.A:
                view.RPC("JoinTeamA", RpcTarget.All, id, playerName);
                break;
            case Team.B:
                view.RPC("JoinTeamB", RpcTarget.All, id, playerName);
                break;
        }
    }

    public void AddBot(Team team)
    {
        switch (team)
        {
            case Team.A:
                view.RPC("AddBotTeamA", RpcTarget.All);
                break;
            case Team.B:
                view.RPC("AddBotTeamB", RpcTarget.All);
                break;
        }
    }

    public void PlayGame()
    {
        view.RPC("DeactiveRoomPnl", RpcTarget.All);
    }

    [PunRPC]
    public void DeactiveRoomPnl()
    {
        this.gameObject.SetActive(false);
        
    }

    [PunRPC]
    public void RemovePlayer(string id)
    {
        if (teamB.ContainsKey(id))
        {
            teamB.Remove (id);
            foreach (var item in m_ListTeamA)
            {
                if (item.id == id)
                {
                    item.gameObject.SetActive(false);
                    break;
                }
            }
        }
        if (teamA.ContainsKey(id))
        {
            teamA.Remove (id);
            foreach (var item in m_ListTeamA)
            {
                if (item.id == id)
                {
                    item.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void RemovePlayerUI(string id, Team team)
    {
        view.RPC("RemovePlayer", RpcTarget.All, id);
    }
}
}
