using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class DoTheHustle : MonoBehaviour
{

    int moveint;
    bool moving;
    string actualText;
    bool placingBuilding;
    PathFinder pf;
    GameObject unitInfo;
    Tile chosenTempTile;
    AudioSource[] moveSound;
    public Sprite buildingDefault;
    public Button buildingButton;
    public GameObject theBuilding;
    GameObject buildingOnMouse;

    private void Start()
    {
        Debug.Log(theBuilding);

        placingBuilding = false;
        moveSound = GetComponents<AudioSource>();
        actualText = "";
        unitInfo = GameObject.FindGameObjectWithTag("UnitInfo");
        unitInfo.GetComponent<Text>().text = actualText;
        buildingButton.GetComponent<Image>().sprite = buildingDefault;
        moving = false;
        moveint = 0;
        chosenTempTile = null;
        buildingButton.transform.SetPositionAndRotation(new Vector3(-40f, buildingButton.transform.position.y), Quaternion.identity);
    }
    private void Update()
    {
        // ha sånn at man skal velge en unit som allerede er laget i Map. Velg den med venstre museknapp
        if (!placingBuilding)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //if det er en person her ... eller kanskje om verdien er en spesifik type så "Velger" man den. F. eks 2 for unit og 3 for bygning.
                Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                int x = (int)(m.x + 0.5f);
                int y = (int)(m.y + 0.5f);

                if (!(x < 0 || y < 0 || y > TileMap.Height || x > TileMap.Width))
                {
                    try
                    {
                        chosenTempTile = TileMap.MapLevel[x, y];
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("For some reason, you cannot choose this tile");
                    }
                    if (chosenTempTile.MyUnit == null)
                    {
                        actualText = "";
                        unitInfo.GetComponent<Text>().text = actualText;
                        buildingButton.transform.SetPositionAndRotation(new Vector3(-40f, buildingButton.transform.position.y), Quaternion.identity);
                    }
                    else
                    {
                        actualText = chosenTempTile.MyUnit.Name;
                        unitInfo.GetComponent<Text>().text = actualText;
                        buildingButton.transform.SetPositionAndRotation(new Vector3(60f, buildingButton.transform.position.y), Quaternion.identity);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && chosenTempTile.MyUnit != null)
            {
                //Movement
                moveSound[0].Play();
                moving = false;
                moveint = 0;
                Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Move(m);
            }
            if (moving && pf.pathMap.Count - moveint > 0)
            {
                Position p = (Position)pf.pathMap[pf.pathMap.Count - moveint - 1];

                chosenTempTile.MyUnit.ElementPosition = p;
                chosenTempTile = TileMap.MapLevel[p.X, p.Y];
                moveint++;
            }
            else
            {
                moving = false;
                moveint = 0;
            }
        }
        else
        {

            Vector2 m = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)(m.x + 0.5f);
            int y = (int)(m.y + 0.5f);

            buildingOnMouse.transform.SetPositionAndRotation(m, Quaternion.identity);

            if (Input.GetMouseButtonDown(0))
            {
                if (!(x < 0 || y < 0 || y > TileMap.Height || x > TileMap.Width))
                {
                    try
                    {
                        chosenTempTile = TileMap.MapLevel[x, y];
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("For some reason, you cannot choose this tile");
                    }
                    if (chosenTempTile.Value == 0)
                    {
                        new PlaceBuilding(new Position(x,y), buildingOnMouse);
                        Debug.Log(TileMap.MapLevel[x,y].Go);
                    }
                }
                actualText = "";
                unitInfo.GetComponent<Text>().text = actualText;
                buildingButton.transform.SetPositionAndRotation(new Vector3(-40f, buildingButton.transform.position.y), Quaternion.identity);
                placingBuilding = false;
                buildingOnMouse.transform.SetPositionAndRotation(new Vector2(-100f, 0f), Quaternion.identity);
            }
        }



    }

    public void FixedUpdate()
    {
    }

    private void Move(Vector3 m)
    {
        int x = (int)(m.x + 0.5f);
        int y = (int)(m.y + 0.5f);

        pf = new PathFinder(chosenTempTile.MyUnit.ElementPosition, new Position(x, y));
        if (pf.pathMap.Count != 0)
        {
            moving = true;
        }
    }

    public void ChooseBuilding()
    {
        placingBuilding = true;
        buildingOnMouse = Instantiate(theBuilding);
    }
}
