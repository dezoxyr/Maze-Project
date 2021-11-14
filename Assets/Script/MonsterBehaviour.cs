using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface m_navmeshsurface;
    private NavMeshAgent m_myNavMeshAgent;
    [SerializeField]
    private GameObject m_monster;
    [SerializeField]
    private int m_randomIA;
    private System.Random m_rnd;

    // Start is called before the first frame update
    void Start()
    {
        if (SC_MainMenu.level != 5)
        {
            m_myNavMeshAgent = m_monster.AddComponent<NavMeshAgent>();
            m_myNavMeshAgent.radius = 0.15f;
            m_myNavMeshAgent.speed = 2.0f;
            m_rnd = new System.Random();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SC_MainMenu.level != 5)
        {
            if (m_rnd.Next() % m_randomIA == 0)
            {
                float x = m_rnd.Next() % SC_MainMenu.level;
                float y = m_rnd.Next() % SC_MainMenu.level;
                m_myNavMeshAgent.SetDestination(new Vector3(x, 0f, y));
            }
        }
        
    }
}
