using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private NavMeshAgent m_myNavMeshAgent;
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_audioClip;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;

    public GameObject p_player;

    // Start is called before the first frame update
    void Start()
    {
        p_player = GameObject.Find("Player");
        m_myNavMeshAgent = p_player.GetComponent<NavMeshAgent>();
        m_Speed = 0.01f;
        m_RotationSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_myNavMeshAgent.velocity += p_player.transform.forward * m_Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            p_player.transform.Rotate(new Vector3(0.0f, m_RotationSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            p_player.transform.Rotate(new Vector3(0.0f, -m_RotationSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_myNavMeshAgent.velocity -= p_player.transform.forward * m_Speed;
        }
    }

    /// <summary>
    /// Brief called on a collision with an object.
    /// If the object is the monster then -> loose game else the object is the end -> win.
    /// </summary>
    /// <param name="end"></param>
    void OnTriggerEnter(Collider end)
    {
        if (end.gameObject.name == "End")
        {
            //SceneManager.LoadScene("SampleScene");
            Debug.Log("GG BG !!");
            SceneManager.LoadScene("MenuScene");
        }

        if (end.gameObject.name == "Monster")
        {
            Debug.Log("Noob !");
            m_audioSource.PlayOneShot(m_audioClip,1f);
            SceneManager.LoadScene("MenuScene");
        }
    }
}
