using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position {

    int x;
    int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int X
    {
        get
        {
            return x;
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
    }
}
