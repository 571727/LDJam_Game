using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    int value;
    Unit myUnit;
    GameObject go;


	public Tile(int value, GameObject go = null, Unit myUnit = null)
    {
        this.value = value;
        this.myUnit = myUnit;
        this.go = go;
    }

    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
        }
    }

    public Unit MyUnit
    {
        get
        {
            return myUnit;
        }
        set
        {
            myUnit = value;
        }
    }

    public GameObject Go
    {
        get
        {
            return go;
        }

        set
        {
            go = value;
        }
    }
}
