using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    GameObject[,] tiles;
    public GameObject goodTile;
    public GameObject takenTile;
    public GameObject camera;
    public static Unit starterUnit;
    public TileMap tilemap;
    private int width;
    private int height;
    public Sprite open;
    public Sprite taken;
    public float cameraSpeed = 10f;
    public Sprite unitSprite;

    void Start()
    {
        width = 16;
        height = 16;
        tilemap = new TileMap(width, height);
        tiles = new GameObject[width, height];
        //Alle taken tiles må ha samme referanse..
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        for (int i = 0; i < width; i++)
        {
            for (int a = 0; a < height; a++)
            {
                if (TileMap.MapLevel[i, a].Value == 0)
                {
                    tiles[i, a] = Instantiate(goodTile);
                    tiles[i, a].GetComponent<SpriteRenderer>().sprite = open;
                }
                else if (TileMap.MapLevel[i, a].Value == 1)
                {
                    tiles[i, a] = Instantiate(takenTile);
                    tiles[i, a].GetComponent<SpriteRenderer>().sprite = taken;
                }
                tiles[i, a].transform.SetPositionAndRotation(new Vector3(i, a, 1f), tiles[i, a].transform.rotation);
                TileMap.MapLevel[i, a].Go = tiles[i, a];
            }
        }
        starterUnit = new Unit(new Position(0, 0), "Unit", unitSprite);
        Debug.Log("1221");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TurnBlack(m);
            Debug.Log(m);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            camera.transform.Translate(new Vector3(0f, 1f) * Time.deltaTime * cameraSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            camera.transform.Translate(new Vector3(0f, -1f) * Time.deltaTime * cameraSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            camera.transform.Translate(new Vector3(-1f, 0f) * Time.deltaTime * cameraSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            camera.transform.Translate(new Vector3(1f, 0f) * Time.deltaTime * cameraSpeed);
        }
    }



    private void TurnBlack(Vector3 m)
    {
        int x = (int)(m.x + 0.5f);
        int y = (int)(m.y + 0.5f);

        if (TileMap.MapLevel[x, y].Value == 0)
        {
            tiles[x, y].GetComponent<SpriteRenderer>().sprite = taken;
            TileMap.MapLevel[x, y].Value = 1;
        }
        else if (TileMap.MapLevel[x, y].Value == 1)
        {
            tiles[x, y].GetComponent<SpriteRenderer>().sprite = open;

            TileMap.MapLevel[x, y].Value = 0;
        }


    }
}
