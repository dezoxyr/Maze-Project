using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{

    private NavMeshAgent myNavMeshAgent;
    public GameObject player;
    float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        myNavMeshAgent = player.GetComponent<NavMeshAgent>();
        m_Speed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            myNavMeshAgent.velocity += player.transform.forward * m_Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Rotate(new Vector3(0.0f, 1.5f, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Rotate(new Vector3(0.0f, -1.5f, 0.0f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            myNavMeshAgent.velocity -= player.transform.forward * m_Speed;
        }
    }
}
