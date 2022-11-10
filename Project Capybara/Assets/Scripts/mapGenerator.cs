using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilesList
{
    bgTile = 0,
    cobbleFloor = 1,
    cobbleWall = 2
}

public class mapGenerator : MonoBehaviour
{
    public Tilemap bg; //background tilemap
    public Tilemap fg; //foreground tilemap

    public List<Tile> tiles;

    public uint startRoomSize = 3;
    public int mapSizeX = 75;
    public int mapSizey = 75;

    public GameObject room;

    // Start is called before the first frame update
    void Start()
    {
        generateStartRoom();

        Instantiate(room,transform.position,Quaternion.identity);
        Instantiate(room, transform.position, Quaternion.identity);
        Instantiate(room, transform.position, Quaternion.identity);
        Instantiate(room, transform.position, Quaternion.identity);
        Instantiate(room, transform.position, Quaternion.identity);
        Instantiate(room, transform.position, Quaternion.identity);

    }

    private void generateStartRoom()
    {
        for (int x = -(mapSizeX / 2); x < mapSizeX / 2; x++)
        {
            for (int y = -(mapSizey / 2); y < mapSizey / 2; y++)
            {
                bg.SetTile(new Vector3Int((int)x, (int)y, 0), tiles[(int)TilesList.bgTile]);
            }
        }

        generateRoomFloor(new Vector3Int(0, 0), TilesList.cobbleFloor, startRoomSize);
        generateRoomWalls(new Vector3Int(0, 0), TilesList.cobbleWall, startRoomSize);
    }

    private void generateRoomFloor(Vector3Int startPos, TilesList floorTile, uint roomSize)
    {
        float half = (roomSize / 2.0f);

        for (float x = -half; x < half; x++)
        {
            for (float y = -half; y < half; y++)
            {
                bg.SetTile(startPos + new Vector3Int((int)x, (int)y, 0), tiles[(int)floorTile]);
            }
        }
    }

    private void generateRoomWalls(Vector3Int startPos, TilesList floorTile, uint roomSize)
    {
        float half = (roomSize / 2.0f);

        for (float x = -half - 1; x < half + 1; x++)
        {
            for (float y = -half - 1; y < half + 1; y++)
            {
                if (x < -half || x >= half || y < -half || y >= half)
                {
                    fg.SetTile(startPos + new Vector3Int((int)x, (int)y, 0), tiles[(int)floorTile]);
                }
            }
        }
    }

    public void generateExternalRoomFloor(Vector3Int startPos, TilesList floorTile, uint roomSize)
    {
        generateRoomFloor(startPos,floorTile,roomSize);
    }

    public void generateExternalRoomWalls(Vector3Int startPos, TilesList floorTile, uint roomSize)
    {
        generateRoomWalls(startPos, floorTile, roomSize);
    }
}