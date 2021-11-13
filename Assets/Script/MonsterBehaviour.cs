using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navmeshsurface;
    private NavMeshAgent myNavMeshAgent;
    [SerializeField]
    private GameObject monster;
    [SerializeField]
    private int randomIA;
    private System.Random rnd;

    // Start is called before the first frame update
    void Start()
    {
        if (SC_MainMenu.level != 5)
        {
            myNavMeshAgent = monster.AddComponent<NavMeshAgent>();
            myNavMeshAgent.radius = 0.15f;
            myNavMeshAgent.speed = 2.0f;
            rnd = new System.Random();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SC_MainMenu.level != 5)
        {
            if (rnd.Next() % randomIA == 0)
            {
                float x = rnd.Next() % SC_MainMenu.level;
                float y = rnd.Next() % SC_MainMenu.level;
                myNavMeshAgent.SetDestination(new Vector3(x, 0f, y));
            }
        }
        
    }
}
