using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
namespace Complete
{
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TankInfos myTankInfo;
    [SerializeField] public RoomPnlControl m_RoomControl;
    [SerializeField] public List<Vector3> m_InitPosA;
    [SerializeField] public List<Vector3> m_InitPosB;
    private float SpawnTime = 5;
    private float startTime;
    public PhotonView view;
    private void Awake() {
        startTime = SpawnTime;
        Instance = this;
    }
    public void StartGame()
    {
        view.RPC("InitTank",RpcTarget.All);
    }
    [PunRPC]
    public void InitTank()
    {
        int i = 0;
        foreach (var player in m_RoomControl.teamA)
        {
            if(PhotonNetwork.LocalPlayer.UserId == player.Key)
            {
                var p = PhotonNetwork.Instantiate("Tank",m_InitPosA[i],Quaternion.Euler(0,-90,0));
                p.GetComponent<TankControl>().SetCamera();
            }
            else if(player.Value.IsBot && PhotonNetwork.IsMasterClient)
            {
                var bot = PhotonNetwork.Instantiate("Tank",m_InitPosA[i],Quaternion.Euler(0,-90,0));
                view.RPC("FindBot",RpcTarget.All,bot.GetComponent<PhotonView>().ViewID);
            }
            i++;
        }
        i = 0;
        foreach (var player in m_RoomControl.teamB)
        {
            if(PhotonNetwork.LocalPlayer.UserId == player.Key)
            {
                var p = PhotonNetwork.Instantiate("Tank",m_InitPosB[i],Quaternion.Euler(0,90,0));
                p.GetComponent<TankControl>().SetCamera();
                
            }
            else if(player.Value.IsBot && PhotonNetwork.IsMasterClient)
            {
                var bot = PhotonNetwork.Instantiate("Tank",m_InitPosB[i],Quaternion.Euler(0,90,0));
                view.RPC("FindBot",RpcTarget.All,bot.GetComponent<PhotonView>().ViewID);
            }
            i++;
        }
    }
    [PunRPC]
    public void FindBot(int viewId)
    {
        var tank = PhotonView.Find(viewId);
        tank.GetComponent<TankControl>().IsBot = true;
    }

    void Update()
    {
        if(startTime <= 0)
        {
            
            PhotonNetwork.Instantiate(Random.Range(0,100) > 50 ? "Health" : "Speed",new Vector3(0,0.6f,0),Quaternion.identity);
            startTime = SpawnTime;
        }
        else
        {
            startTime -= Time.deltaTime;
        }
    }
    }

    public void startGame()
    {
        // Init for team 1
        for (int i = 0; i < 4; i++)
        {
            GameObject tank = Instantiate(tankSample);
            string name = "A" + i.ToString();
            tank.GetComponent<TankInfo>().Init(Random.Range(0, 100000000), name, Team.A, m_myName, name != m_myName, new Vector3(Random.Range(-45, -35), 0, Random.Range(35, 45)));
            m_team1Tank.Add(tank);
        }
        // Init for team 2
        for (int i = 0; i < 4; i++)
        {
            GameObject tank = Instantiate(tankSample);
            string name = "B" + i.ToString();
            tank.GetComponent<TankInfo>().Init(Random.Range(0, 100000000), name, Team.B, m_myName, name != m_myName, new Vector3(Random.Range(35, 45), 0, Random.Range(-45, -35)));
            m_team2Tank.Add(tank);
        }
    }
}
