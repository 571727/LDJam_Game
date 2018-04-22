using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaceBuilding {

	public PlaceBuilding(Position p, GameObject theBuilding)
    {
        TileMap.MapLevel[p.X, p.Y].Value = 1;
        TileMap.MapLevel[p.X, p.Y].Go.GetComponent<SpriteRenderer>().sprite = theBuilding.GetComponent<Sprite>();
    }
}
