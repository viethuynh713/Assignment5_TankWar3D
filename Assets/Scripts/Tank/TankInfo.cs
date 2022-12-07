using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankInfo : MonoBehaviour
{
    public string m_name;
    public Team m_team;
    public bool m_isMyPlayer;
    public bool m_isBot;
    public int m_ID;

    public bool IsMyPlayer { get { return m_isMyPlayer; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int ID, string name, Team team, string myName, bool isBot, Vector3 position)
    {
        // set info
        m_ID = ID;
        m_name = name;
        m_team = team;
        m_isMyPlayer = name == myName;
        m_isBot = isBot;
        // set camera
        if (m_isMyPlayer)
        {
            GameObject camera = GameObject.FindWithTag("MainCamera");
            Vector3 pos = camera.transform.position;
            camera.transform.parent = transform;
            camera.transform.localPosition = pos + new Vector3(0, 0, -2);
        }
        transform.position = position;
    }
}
