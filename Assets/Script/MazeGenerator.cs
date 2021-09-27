using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator{

    private int width;
    private int height;

    enum Direction
    {
        N = 1,
        S = 2,
        E = 4,
        W = 8
    }

    //constructor, take width and height of the maze
    public MazeGenerator(int w = 10, int h = 10)
    {
        this.width = w;
        this.height = h;
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
