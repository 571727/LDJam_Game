using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap {
    public static Tile[,] MapLevel;
    private static int width;
    private static int height;

	public TileMap(int width, int height)
    {
        TileMap.width = width;
        TileMap.height = height;
        MapLevel = new Tile[width, height];

        for (int i = 0; i < Width; i++)
        {
            for (int a = 0; a < Height; a++)
            {
                MapLevel[i, a] = new Tile(0);
            }
        }
    }

    public static int Width
    {
        get
        {
            return width;
        }
    }
    public static int Height
    {
        get
        {
            return height;
        }
    }
}
