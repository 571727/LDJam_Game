using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{

    private Sprite unitSprite;
    GameObject element;
    Position p;
    string name;
    public Unit(Position p, string name, Sprite unitSprite)
    {
        this.unitSprite = unitSprite;
        this.name = name;
        this.p = p;

        element = new GameObject();
        element.AddComponent<SpriteRenderer>();
        
        element.GetComponent<SpriteRenderer>().sprite = unitSprite;

        TileMap.MapLevel[p.X, p.Y].MyUnit = this;
        TileMap.MapLevel[p.X, p.Y].Value = 2;

    }

    public Position ElementPosition
    {
        get
        {
            return p;
        }
        set
        {
            TileMap.MapLevel[p.X, p.Y].MyUnit = null;
            TileMap.MapLevel[p.X, p.Y].Value = 0;
            p = value;
            TileMap.MapLevel[p.X, p.Y].MyUnit = this;
            TileMap.MapLevel[p.X, p.Y].Value = 2;
            element.transform.SetPositionAndRotation(new Vector3(p.X, p.Y), element.transform.rotation);
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
    }
}
