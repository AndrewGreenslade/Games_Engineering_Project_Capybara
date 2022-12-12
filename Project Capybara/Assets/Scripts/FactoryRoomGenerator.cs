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

public class FactoryRoomGenerator : MonoBehaviour
{
    public Tilemap bg; //background tilemap
    public Tilemap fg; //foreground tilemap
    public Tilemap darkness; //foreground tilemap

    public List<Tile> tiles;
    public List<GameObject> Rooms;
    public List<GameObject> powerUps;
    public GameObject exitRoom;
    public GameObject exitRoomKey;

    public uint startRoomSize = 3;
    public int mapSizeX = 75;
    public int mapSizey = 75;
    public int RoomsTogenerate = 5;
    public int RoomsGenerated = 0;

    public GameObject room;
    public bool pathGenerated = false;
    public bool areRoomsgenerated = false;

    // Start is called before the first frame update
    void Start()
    {
        generateStartRoom();

        StartCoroutine(spawnRooms());
    }

    private void Update()
    {
        if(!pathGenerated && !areRoomsgenerated)
        {
            if (RoomsGenerated >= RoomsTogenerate)
            {
                if (Rooms[Rooms.Count - 1].GetComponent<RoomReposition>().isGenerated)
                {
                    areRoomsgenerated = true;
                }
            }
        }

        if (!pathGenerated && areRoomsgenerated)
        {
            GeneratePaths();
            pathGenerated = true;

            Instantiate(exitRoomKey, Rooms[1].GetComponent<RoomReposition>().transform.position, Quaternion.identity);
            Instantiate(exitRoom,Rooms[Rooms.Count - 1].GetComponent<RoomReposition>().transform.position,Quaternion.identity);
        }
    }

    IEnumerator spawnRooms()
    {
        GameObject rooms = Instantiate(room, transform.position, Quaternion.identity);
        Rooms.Add(rooms);

        yield return new WaitForSeconds(RoomReposition.spawnTime);
        
        RoomsGenerated++;

        if (RoomsGenerated < RoomsTogenerate)
        {
            StartCoroutine(spawnRooms());
        }
    }

    private void generateStartRoom()
    {
        for (int x = -(mapSizeX / 2); x < mapSizeX / 2; x++)
        {
            for (int y = -(mapSizey / 2); y < mapSizey / 2; y++)
            {
                darkness.SetTile(new Vector3Int((int)x, (int)y, 0), tiles[(int)TilesList.bgTile]);
            }
        }

        Rooms.Add(gameObject);

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
                darkness.SetTile(startPos + new Vector3Int((int)x, (int)y, 0), null);
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
                    darkness.SetTile(startPos + new Vector3Int((int)x, (int)y, 0), null);
                    fg.SetTile(startPos + new Vector3Int((int)x, (int)y, 0), tiles[(int)floorTile]);
                }
            }
        }
    }

    private void generatePath(Vector2 t_pos1, Vector2 t_pos2, TilesList floorTile)
    {
        int currentX = (int)t_pos1.x;
        int currentY = (int)t_pos1.y;

        while(currentX != t_pos2.x)
        {
            bg.SetTile(new Vector3Int(currentX, currentY , 0), tiles[(int)floorTile]);
            
            fg.SetTile(new Vector3Int(currentX, currentY, 0), null);
            darkness.SetTile(new Vector3Int(currentX, currentY, 0), null);

            if (currentX <= t_pos2.x)
            {
                currentX++;
            }
            else
            {
                currentX--;
            }
        }

        while (currentY != t_pos2.y)
        {
            bg.SetTile(new Vector3Int(currentX, currentY, 0), tiles[(int)floorTile]);

            fg.SetTile(new Vector3Int(currentX, currentY, 0), null);
            darkness.SetTile(new Vector3Int(currentX, currentY, 0), null);

            if (currentY <= t_pos2.y)
            {
                currentY++;
            }
            else
            {
                currentY--;
            }
        }
    }

    private void GeneratePaths()
    {

        for (int i = 0; i < Rooms.Count; i++)
        {
            if(i < Rooms.Count - 1)
            {
                generatePath(Rooms[i].transform.position, Rooms[i + 1].transform.position, TilesList.cobbleFloor);
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