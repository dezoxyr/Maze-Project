using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;


public class LevelManager : MonoBehaviour
{
    public GameObject ground;
    public GameObject wall;
    public GameObject parent;
    public GameObject player;
    public GameObject end;

    private NavMeshAgent myNavMeshAgent;
    private const int width = 10;
    private const int height = 10;
    private float distance = 0.0f;
    private Vector3 pos;
    private int[,] maze = new int[width,height];
    private int[,] deadEnd = new int[width, height];


    /// <summary>
    /// Brief instanciate the maze and the array containing the deadend infos
    /// </summary>
    private void createMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = 0;
                deadEnd[i, j] = 0;
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
                    GameObject g = Instantiate(wall, new Vector3((float)(y - 0.45), 0.5f, z), Quaternion.Euler(0f, 90f, 0f), parent.transform);
                    g.name = y + "," + z+" cote : N";
                }
                if ((x & 2) == 2)
                {
                    GameObject g = Instantiate(wall, new Vector3((float)(y + 0.45), 0.5f, z), Quaternion.Euler(0f, 90f, 0f), parent.transform);
                    g.name = y + "," + z + "cote : S";
                }
                if ((x & 4) == 4)
                {
                    GameObject g = Instantiate(wall, new Vector3( y, 0.5f, (float)(z+0.45)), Quaternion.identity, parent.transform);
                    g.name = y + "," + z + "cote : E";
                }
                if ((x & 8) == 8)
                {
                    GameObject g = Instantiate(wall, new Vector3(y, 0.5f, (float)(z - 0.45)), Quaternion.identity, parent.transform);
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
        GameObject spawn = Instantiate(ground, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        spawn.name = "spawn";
        genWall(~maze[0,0], 0, 0);
        player.transform.localPosition = new Vector3(spawn.transform.localPosition.x, spawn.transform.localPosition.y+0.5f, spawn.transform.localPosition.z);
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (!(i == 0 && j == 0))
                {
                    GameObject g = Instantiate(ground, new Vector3(i, 0, j), Quaternion.identity, parent.transform);
                    //g.name = i + "," + j;
                    genWall(~maze[i, j], i, j);

                }
            }
        }
        return true;
    }

    private void findDeadEnd()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if (maze[i,j]==1 || maze[i, j] == 2 || maze[i, j] == 4 || maze[i, j] == 8)
                {
                    deadEnd[i, j] = 1;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        createMaze();
        
        MazeGenerator m = new MazeGenerator(width,height);
        System.Random rnd = new System.Random();
        maze = m.generate(0, 0, maze,rnd);
        findDeadEnd();
        genMaze(maze);

        //NavMeshSurface s = parent.AddComponent<NavMeshSurface>();
        NavMeshSurface s = parent.GetComponent<NavMeshSurface>();
        s.BuildNavMesh();
        myNavMeshAgent = player.AddComponent<NavMeshAgent>();
        myNavMeshAgent.radius = 0.15f;
        myNavMeshAgent.speed = 2.5f;
        //myNavMeshAgent.angularSpeed = 60;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (deadEnd[i, j] == 1)
                {
                    //Debug.Log("Impasse :"+i+","+j);
                    NavMeshPath path = new NavMeshPath();
                    myNavMeshAgent.CalculatePath(new Vector3(i, 0.2f, j), path);
                    myNavMeshAgent.SetPath(path);
                    //pos = new Vector3(i, 0.2f, j);
                    float newDistance = ExtensionMethod.GetPathRemainingDistance(myNavMeshAgent);
                    //Debug.Log("Distance :"+newDistance);
                    if (newDistance > distance)
                    {
                        distance = newDistance;
                        pos = new Vector3(i, 0.2f, j);
                    }
                }
            }
        }
        //Debug.Log(pos+" "+distance);
        myNavMeshAgent.Warp(new Vector3(0f, 0f, 0f));
        //myNavMeshAgent.SetDestination(pos);
        end.transform.localPosition = new Vector3(pos.x, 0.5f, pos.z);

#if UNITY_EDITOR
        Debug.Log("Creating maze file...");
        string texte = null;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                texte = texte + maze[i, j]+"/"+deadEnd[i,j]+" ";
            }
            texte = texte + "\r";
        }
        File.WriteAllText("WriteText.txt", texte);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
