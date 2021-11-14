using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    private NavMeshAgent m_myNavMeshAgent;
    private GameObject[] m_pauseObjects;
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_audioClip;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;

    public GameObject p_player;
    public GameObject p_looseMenu;
    public GameObject p_winMenu;

    // Start is called before the first frame update
    void Start()
    {
        p_player = GameObject.Find("Player");
        m_myNavMeshAgent = p_player.GetComponent<NavMeshAgent>();
        m_Speed = 0.01f;
        m_RotationSpeed = 1.0f;
        m_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
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
            Debug.Log("GG BG !!");
            foreach (GameObject g in m_pauseObjects)
            {
                g.SetActive(false);
            }
            p_winMenu.SetActive(true);
            //SceneManager.LoadScene("MenuScene");
        }

        if (end.gameObject.name == "Monster")
        {
            m_audioSource.PlayOneShot(m_audioClip, 1f);
            StartCoroutine(playerLost());
        }
    }

    private IEnumerator playerLost()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Noob !");
        foreach (GameObject g in m_pauseObjects)
        {
            g.SetActive(false);
        }
        p_looseMenu.SetActive(true);
        //SceneManager.LoadScene("MenuScene");
    }
}
