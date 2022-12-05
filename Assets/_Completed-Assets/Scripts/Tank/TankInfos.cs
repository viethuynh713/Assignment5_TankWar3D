using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Team{
    A,
    B,
}
public class TankInfos 
{

    public string PlayerName;
    public bool IsBot;
    public Team MyTeam;
     
    public TankInfos(string name, bool isBot,Team team)
    {
        this.PlayerName = name;
        this.IsBot = isBot;
        this.MyTeam = team;
    }
}
