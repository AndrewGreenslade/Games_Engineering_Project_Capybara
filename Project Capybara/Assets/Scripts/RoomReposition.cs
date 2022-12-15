using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReposition : MonoBehaviour
{
    public RoomGenerator mapGen;
    public float roomTime;
    public uint roomSize = 6;
    private bool shoudIncreaseTime = false;
    public bool isGenerated = false;
    public Vector2 MaxMapSize;
    public Vector2 MinMapSize;
    public static float spawnTime = 0.5f;
    public List<GameObject> collisions = new List<GameObject>();

    private void Awake()
    {
        mapGen = FindObjectOfType<RoomGenerator>();

        GetComponent<BoxCollider2D>().size = new Vector2(roomSize + 6,roomSize + 6);
        
        MaxMapSize = new Vector2((mapGen.mapSizeX / 2.0f) - roomSize, (mapGen.mapSizeX / 2.0f) - roomSize);
        MinMapSize = new Vector2(-(mapGen.mapSizeX / 2.0f) + roomSize, -(mapGen.mapSizey / 2.0f) + roomSize);

        RepositionRoom();

        shoudIncreaseTime = true;
    }

    private void Update()
    {
        if (collisions.Count <= 0)
        {
            if (!isGenerated)
            {
                if (shoudIncreaseTime && roomTime < spawnTime)
                {
                    roomTime += Time.deltaTime;
                }

                if (roomTime >= spawnTime)
                {
                    mapGen.generateExternalRoomWalls(new Vector3Int((int)transform.position.x, (int)transform.position.y , 0), TilesList.cobbleWall, roomSize);
                    mapGen.generateExternalRoomFloor(new Vector3Int((int)transform.position.x, (int)transform.position.y , 0), TilesList.cobbleFloor, roomSize);
                    isGenerated = true;
                    shoudIncreaseTime = false;
                }
            }
        }
        else if (collisions.Count > 0 && !isGenerated)
        {
            roomTime = 0;
            shoudIncreaseTime = false;
            RepositionRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            if (!collisions.Contains(collision.gameObject))
            {
                collisions.Add(collision.gameObject);
            }
        }
    }

    private void RepositionRoom()
    {
        int X = (int)Random.Range(MinMapSize.x, MaxMapSize.x);
        int Y = (int)Random.Range(MinMapSize.y, MaxMapSize.y);

        transform.position = new Vector2(X, Y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            if (collisions.Contains(collision.gameObject))
            {
                collisions.Remove(collision.gameObject);
                roomTime = 0;
                shoudIncreaseTime = true;
            }
        }
    }
}
