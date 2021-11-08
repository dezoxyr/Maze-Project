using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class MazeGenerator
{

    private int width;
    private int height;

    private int[,] Dx = new int[2, 4] { { 1, 2, 4, 8 }, { 0, 0, 1, -1 } };
    private int[,] Dy = new int[2, 4] { { 1, 2, 4, 8 }, { -1, 1, 0, 0 } };
    private int[,] Op = new int[2, 4] { { 1, 2, 4, 8 }, { 2, 1, 8, 4 } };

    private enum Direction
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8
    }
    /// <summary>
    /// Brief to convert a direction regarding the mode selectionned
    /// </summary>
    /// <param name="x">The direction</param>
    /// <param name="mode">The mode</param>
    /// <returns>The converted direction</returns>
    private int dirConvert(int x, int mode)
    {
        int nb = 0;
        if (mode == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Dx[0, i] == x)
                {
                    nb = Dx[1, i];
                }
            }
        }
        else if (mode == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Dy[0, i] == x)
                {
                    nb = Dy[1, i];
                }
            }
        }
        else if (mode == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Op[0, i] == x)
                {
                    nb = Op[1, i];
                }
            }
        }
        return nb;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="w">Maze width, default 10</param>
    /// <param name="h">Maze height, default 10</param>
    public MazeGenerator(int w = 10, int h = 10)
    {
        this.width = w;
        this.height = h;
    }

    /// <summary>
    /// Brief to create the maze array
    /// </summary>
    /// <param name="x">Starting pos</param>
    /// <param name="y">Starting pos</param>
    /// <param name="maze">Empty maze array</param>
    /// <param name="rnd">Random seed</param>
    /// <returns>Return the filled maze array</returns>
    public int[,] generate(int x, int y, int[,] maze, System.Random rnd)
    {
        //Randomisation
        Type type = typeof(Direction);
        Array dir = type.GetEnumValues();
        for (int i = 0; i < dir.Length; i++)
        {
            int k = rnd.Next(4);
            int tmp = (int)dir.GetValue(i);
            dir.SetValue(dir.GetValue(k), i);
            dir.SetValue(tmp, k);
        }
        //
        for (int i = 0; i < dir.Length; i++)
        {
            int value = (int)dir.GetValue(i);
            int xi = x + dirConvert(value, 0);
            int yi = y + dirConvert(value, 1);

            if (0 <= xi && xi < width && 0 <= yi && yi < height)
            {
                if (maze[yi, xi] == 0)
                {
                    maze[y, x] += value;
                    maze[yi, xi] += dirConvert(value, 2);
                    maze = generate(xi, yi, maze, rnd);
                }
            }

        }

        return maze;
    }

    //getter & setter
    public int getWidth()
    {
        return this.width;
    }
    public int getHeight()
    {
        return this.height;
    }
    public void setWidth(int w)
    {
        this.width = w;
    }
    public void setHeight(int h)
    {
        this.height = h;
    }

}