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

    private const int width = 10;
    private const int height = 10;
    private int[,] maze = new int[width,height];

    /// <summary>
    /// Brief instanciate the maze
    /// </summary>
    private void createMaze()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = 0;
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
                    GameObject g = Instantiate(wall, new Vector3((float)(y - 0.5), 0.5f, z), Quaternion.Euler(0f, 90f, 0f));
                    g.name = y + "," + z+" cote : N";
                    g.transform.parent = parent.transform;
                }
                if ((x & 2) == 2)
                {
                    GameObject g = Instantiate(wall, new Vector3((float)(y + 0.5), 0.5f, z), Quaternion.Euler(0f, 90f, 0f));
                    g.name = y + "," + z + "cote : S";
                    g.transform.parent = parent.transform;
                }
                if ((x & 4) == 4)
                {
                    GameObject g = Instantiate(wall, new Vector3( y, 0.5f, (float)(z+0.5)), Quaternion.identity);
                    g.name = y + "," + z + "cote : E";
                    g.transform.parent = parent.transform;
                }
                if ((x & 8) == 8)
                {
                    GameObject g = Instantiate(wall, new Vector3(y, 0.5f, (float)(z - 0.5)), Quaternion.identity);
                    g.name = y + "," + z + "cote : W";
                    g.transform.parent = parent.transform;
                }
                

    }

    /// <summary>
    /// Brief to generate the full maze
    /// </summary>
    /// <param name="maze">Maze array</param>
    /// <returns>True if generation is ok</returns>
    private bool genMaze(int[,] maze)
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GameObject g = Instantiate(ground, new Vector3(i, 0, j), Quaternion.identity);
                g.name = i + "," + j;
                g.transform.parent = parent.transform;
                genWall(~maze[i,j],i,j);

                NavMeshSurface s = parent.AddComponent(typeof(NavMeshSurface)) as NavMeshSurface;
                s.BuildNavMesh();
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        createMaze();

        MazeGenerator m = new MazeGenerator(width,height);
        System.Random rnd = new System.Random();
        maze = m.generate(0, 0, maze,rnd);
        genMaze(maze);



#if UNITY_EDITOR
        Debug.Log("Creating maze file...");
        string texte = null;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                texte = texte + maze[i, j]+" ";
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
