using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private int[,] maze = new int[10,10];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                maze[i, j] = 0;
            }
        }
        MazeGenerator m = new MazeGenerator(10,10);
        maze = m.generate(0, 0, maze);

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
