using System.Collections;
using System.Collections.Generic;
using System;

public class MazeGenerator{

    private int width;
    private int height;

    private enum Direction
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8
    }
    private enum Dy
    {
        N = -1,
        S = 1,
        E = 0,
        W = 0
    }
    private enum Dx
    {
        N = 0,
        S = 0,
        E = 1,
        W = -1
    }
    private enum Op
    {
        N = Direction.S,
        S = Direction.N,
        E = Direction.W,
        W = Direction.E
    }

    //constructor, take width and height of the maze
    public MazeGenerator(int w = 10, int h = 10)
    {
        this.width = w;
        this.height = h;

    }

    public int[,] generate(int x, int y, int[,] maze)
    {
        //Randomisation
        Type type = typeof(Direction);
        Array dir = type.GetEnumValues();
        System.Random rnd = new System.Random();
        int n = dir.Length;
        while(n > 1){
            int k = rnd.Next(n--);
            int tmp = (int)dir.GetValue(n);
            dir.SetValue(dir.GetValue(k), n);
            dir.SetValue(tmp, k);
        }

        for(int i = 0; i < dir.Length; i++)
        {
            int xi = x + (int)(Dx)(int)dir.GetValue(i);
            int yi = y + (int)(Dy)(int)dir.GetValue(i);

            if (0 <= x && x < this.width && 0 <= y && y <= this.height && maze[x,y] == 0)
            {
                maze[x,y] += (int)(Direction)(int)dir.GetValue(i);
                maze[xi,yi] += (int)(Op)(int)dir.GetValue(i);
                maze = generate(xi, yi, maze);
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
