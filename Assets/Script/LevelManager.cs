using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public GameObject p_ground;
    public GameObject p_wall;
    public GameObject p_parent;
    public GameObject p_player;
    public GameObject p_end;
    public GameObject p_monster;
    public GameObject p_pauseMenu;

    private NavMeshAgent m_myNavMeshAgent;
    private static int m_width = SC_MainMenu.level;
    private static int m_height = SC_MainMenu.level;
    private float m_distance = 0.0f;
    private Vector3 m_pos;
    private int[,] m_maze = new int[m_width,m_height];
    private int[,] m_deadEnd = new int[m_width, m_height];
    private GameObject[] m_pauseObjects;


    /// <summary>
    /// Brief instanciate the maze and the array containing the deadend infos
    /// </summary>
    private void createMaze()
    {
        for (int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                m_maze[i, j] = 0;
                m_deadEnd[i, j] = 0;
            }
        }
    }

    /// <summary>
    /// Brief to generate the walls of a maze cell
    /// </summary>
    /// <param name="x">Value of the cell</param>
    /// <param name="y">X position of the cell</param>
    /// <param name="z">Z position of the cell</param>
    private void genWall(int x, int y, int z)
    {
                if ((x & 1) == 1)
                {
                    GameObject g = Instantiate(p_wall, new Vector3((float)(y - 0.45), 0.0f, z), Quaternion.Euler(0f, 90f, 0f), p_parent.transform);
                    g.name = y + "," + z+" cote : N";
                }
                if ((x & 2) == 2)
                {
                    GameObject g = Instantiate(p_wall, new Vector3((float)(y + 0.45), 0.0f, z), Quaternion.Euler(0f, 90f, 0f), p_parent.transform);
                    g.name = y + "," + z + "cote : S";
                }
                if ((x & 4) == 4)
                {
                    GameObject g = Instantiate(p_wall, new Vector3( y, 0.0f, (float)(z+0.45)), Quaternion.identity, p_parent.transform);
                    g.name = y + "," + z + "cote : E";
                }
                if ((x & 8) == 8)
                {
                    GameObject g = Instantiate(p_wall, new Vector3(y, 0.0f, (float)(z - 0.45)), Quaternion.identity, p_parent.transform);
                    g.name = y + "," + z + "cote : W";
                }
                

    }

    /// <summary>
    /// Brief to generate the full maze
    /// </summary>
    /// <param name="maze">Maze array</param>
    /// <returns>True if generation is ok</returns>
    private bool genMaze(int[,] maze)
    {
        GameObject spawn = Instantiate(p_ground, new Vector3(0, 0, 0), Quaternion.identity, p_parent.transform);
        spawn.name = "spawn";
        genWall(~maze[0,0], 0, 0);
        p_player.transform.localPosition = new Vector3(spawn.transform.localPosition.x, spawn.transform.localPosition.y, spawn.transform.localPosition.z);
        for (int i = 0; i < m_width; i++)
        {
            for(int j = 0; j < m_height; j++)
            {
                if (!(i == 0 && j == 0))
                {
                    GameObject g = Instantiate(p_ground, new Vector3(i, 0, j), Quaternion.identity, p_parent.transform);
                    //g.name = i + "," + j;
                    genWall(~maze[i, j], i, j);

                }
            }
        }
        return true;
    }

    /// <summary>
    /// Brief to find the dead end of the maze.
    /// It fills an array containing a 1 if the cell is a dead end.
    /// </summary>
    private void findDeadEnd()
    {
        for(int i = 0; i < m_width; i++)
        {
            for(int j = 0; j < m_height; j++)
            {
                if (m_maze[i,j]==1 || m_maze[i, j] == 2 || m_maze[i, j] == 4 || m_maze[i, j] == 8)
                {
                    m_deadEnd[i, j] = 1;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        createMaze();

        MazeGenerator m = new MazeGenerator(m_width,m_height);
        System.Random rnd = new System.Random();
        m_maze = m.generate(0, 0, m_maze,rnd);
        findDeadEnd();
        genMaze(m_maze);

        NavMeshSurface s = p_parent.GetComponent<NavMeshSurface>();
        s.BuildNavMesh();
        m_myNavMeshAgent = p_player.AddComponent<NavMeshAgent>();
        m_myNavMeshAgent.radius = 0.15f;
        m_myNavMeshAgent.speed = 1.5f;
        //myNavMeshAgent.angularSpeed = 60;
        for (int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                if (m_deadEnd[i, j] == 1)
                {

                    NavMeshPath path = new NavMeshPath();
                    m_myNavMeshAgent.CalculatePath(new Vector3(i, 0.2f, j), path);
                    m_myNavMeshAgent.SetPath(path);
                    //pos = new Vector3(i, 0.2f, j);
                    float newDistance = ExtensionMethod.GetPathRemainingDistance(m_myNavMeshAgent);
                    //Debug.Log("Distance :"+newDistance);
                    if (newDistance > m_distance)
                    {
                        m_distance = newDistance;
                        m_pos = new Vector3(i, 0.2f, j);
                    }
                }
            }
        }
        //Debug.Log(pos+" "+distance);
        m_myNavMeshAgent.Warp(new Vector3(0f, 0f, 0f));
        //myNavMeshAgent.SetDestination(pos);
        p_end.transform.localPosition = new Vector3(m_pos.x, 0.5f, m_pos.z);
        if (m_maze[(int)m_pos.x,(int)m_pos.z]==1){
            p_end.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        }else if (m_maze[(int)m_pos.x, (int)m_pos.z] == 2){
            //Already good orientation
        }else if (m_maze[(int)m_pos.x, (int)m_pos.z] == 4){
            p_end.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }else if (m_maze[(int)m_pos.x, (int)m_pos.z] == 8){
            p_end.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
        if (SC_MainMenu.level != 5)
        {
            p_monster.transform.localPosition = new Vector3(m_pos.x - 1, 0f, m_pos.z);
        }
        
        m_pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
#if UNITY_EDITOR
        Debug.Log("Creating maze file...");
        string texte = null;
        for(int i = 0; i < m_height; i++)
        {
            for(int j = 0; j < m_width; j++)
            {
                texte = texte + m_maze[i, j]+"/"+m_deadEnd[i,j]+" ";
            }
            texte = texte + "\r";
        }
        File.WriteAllText("WriteText.txt", texte);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!p_pauseMenu.activeSelf)
            {
                p_pauseMenu.SetActive(true);
                foreach (GameObject g in m_pauseObjects)
                {
                    g.SetActive(false);
                }
            }
            else{
                p_pauseMenu.SetActive(false);
                foreach (GameObject g in m_pauseObjects)
                {
                    g.SetActive(true);
                }
            }
            
        }
        /*if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }*/
    }

    /// <summary>
    /// Brief to reload the scene
    /// </summary>
    public void Reload()
    {
        Debug.Log("reloading");
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Biref to quit to the main menu
    /// </summary>
    public void QuitButton()
    {
        // Quit Game
        SceneManager.LoadScene("MenuScene");
    }

}
