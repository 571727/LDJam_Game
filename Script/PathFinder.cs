using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder{

    public ArrayList pathMap;
    //Old Position
    private Position op;
    //New Position
    private Position np;

    public int[,] tried;

	public PathFinder(Position op, Position np)
    {
        this.op = op;
        this.np = np;

        tried = new int[TileMap.Width, TileMap.Height];

        FindThePath(op.X, op.Y, 0);
    }

    public bool FindThePath(int x, int y, int steps)
    {
        
        pathMap = new ArrayList();
        bool done = false;
        steps++;

        Debug.Log("X: " + x + ", Y: " + y);
        
        if(CheckTile(x, y))
        {
            tried[x, y] = 1;
            if (x == np.X && y == np.Y)
            {
                done = true;
            }
            else
            {
                //Finn den mest optimale retningen å gå først.
                if (np.Y - y > 0 && np.X - x > 0)
                {
                    //Går nord og øst
                    done = FindThePath(x, y + 1, steps);
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                }
                else if (np.Y - y < 0 && np.X - x < 0)
                {
                    //Går sør og vest
                    done = FindThePath(x - 1,y, steps);
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y + 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                }
                else if (np.Y - y > 0 && np.X - x < 0)
                {
                    //Går nord og vest
                    done = FindThePath(x, y + 1, steps);
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                }
                else if (np.Y - y < 0 && np.X - x > 0)
                {
                    //Går sør og øst
                    done = FindThePath(x + 1, y, steps);
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y + 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                }
                else if (np.Y - y == 0 && np.X - x > 0)
                {
                    //Går bare øst
                    done = FindThePath(x + 1, y, steps);
                    if (!done)
                    {
                        done = FindThePath(x, y + 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                }
                else if (np.Y - y == 0 && np.X - x < 0)
                {
                    //Går bare vest
                    done = FindThePath(x - 1, y, steps);
                    if (!done)
                    {
                        done = FindThePath(x, y + 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                }
                else if (np.Y - y < 0 && np.X - x == 0)
                {
                    //Går sør
                    done = FindThePath(x, y - 1, steps);
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y + 1, steps);
                    }
                }
                else if (np.Y - y > 0 && np.X - x == 0)
                {
                    //Går nord
                    done = FindThePath(x, y + 1, steps);
                    if (!done)
                    {
                        done = FindThePath(x + 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x - 1, y, steps);
                    }
                    if (!done)
                    {
                        done = FindThePath(x, y - 1, steps);
                    }
                }

            }
            if (done)
            {
                pathMap.Add(new Position(x, y));
                Debug.Log("Path found: " + x + ", " + y);
            }
        }

        try
        {
            tried[x, y] = 1;
        }
        catch(IndexOutOfRangeException e)
        {
            Debug.LogError("Prøvde å lagre utenfor indexen");
        }
        return done;
    }

    public bool CheckTile(int x, int y)
    {
        if (x < 0 || y < 0 || y > TileMap.Height || x > TileMap.Width)
        {
            return false;
        }
        else if (TileMap.MapLevel[x, y].Value == 1 || tried[x, y] == 1)
        {
            return false;
        }
        else
            return true;
    }
}
