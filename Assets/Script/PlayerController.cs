using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private NavMeshAgent myNavMeshAgent;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    public GameObject player;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        myNavMeshAgent = player.GetComponent<NavMeshAgent>();
        m_Speed = 0.01f;
        m_RotationSpeed = 1.0f;
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
            player.transform.Rotate(new Vector3(0.0f, m_RotationSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Rotate(new Vector3(0.0f, -m_RotationSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            myNavMeshAgent.velocity -= player.transform.forward * m_Speed;
        }
    }

    void OnTriggerEnter(Collider end)
    {
        if (end.gameObject.name == "End")
        {
            //SceneManager.LoadScene("SampleScene");
            Debug.Log("GG BG !!");
        }

        if (end.gameObject.name == "Monster")
        {
            Debug.Log("Noob !");
            audioSource.PlayOneShot(audioClip,1f);
        }
    }
}
