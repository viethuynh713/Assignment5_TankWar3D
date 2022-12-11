using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TankInfos myTankInfo;
    public List<GameObject> m_team1Tank;
    public List<GameObject> m_team2Tank;
    public string m_myName;

    [SerializeField] public GameObject tankSample;
    private void Awake() {
        Instance = this;
    }
    void Start()
    {
        m_myName = "A2";
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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
