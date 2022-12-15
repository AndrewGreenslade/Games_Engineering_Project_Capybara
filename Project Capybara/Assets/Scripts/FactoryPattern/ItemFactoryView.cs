using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    // creating item
    public GameObject CreateItem(string name, GameObject obj, Vector2 pos)
    {
        GameObject item = Object.Instantiate(obj, pos, Quaternion.identity);
        item.name = name;
        return item;
    }
}

public class ItemFactoryView : MonoBehaviour
{
    public List<GameObject> PowerUpItems = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject key;
    public GameObject sword;
    public GameObject axe;

    public bool SpecialSpawned = false;

    public bool itemsGenerated = false;
    
    public int LevelNumber;
    public int MaxEnemies = 3;
    public int EnemiesCount = 0;

    public List<int> roomsFilled = new List<int>();

    private void LateUpdate()
    {
        RoomGenerator roomGen = FindObjectOfType<RoomGenerator>();

        if (roomGen.areRoomsgenerated && roomGen.pathGenerated && !itemsGenerated)
        {
            ItemFactory itemFactory = new ItemFactory();
            int roomsCount = roomGen.RoomsTogenerate;

            for (int i = 1; i < roomsCount + 1; i++)
            {
                GameObject room = roomGen.Rooms[i];
                RoomReposition roomScript = room.GetComponent<RoomReposition>();

                int X = (int)Random.Range(roomScript.transform.position.x - ((roomScript.roomSize - 1) / 2), roomScript.transform.position.x + ((roomScript.roomSize - 1) / 2));
                int Y = (int)Random.Range(roomScript.transform.position.y - ((roomScript.roomSize - 1) / 2), roomScript.transform.position.y + ((roomScript.roomSize - 1) / 2));

                int SpawnPowerupOrEnemy = Random.Range(0, 2);

                if (i == 1)
                {
                    itemFactory.CreateItem("Key", key, new Vector2(X, Y));
                    roomsFilled.Add(i);
                    continue;
                }
                else if (LevelNumber == 2 && !SpecialSpawned)
                {
                    itemFactory.CreateItem("Sword", sword, new Vector2(X, Y));
                    SpecialSpawned = true;
                    roomsFilled.Add(i);
                    continue;
                }
                else if (LevelNumber == 3 && !SpecialSpawned)
                {
                    itemFactory.CreateItem("Axe", axe, new Vector2(X, Y));
                    SpecialSpawned = true;
                    roomsFilled.Add(i);
                    continue;
                }
                else
                //Spawn powerup
                if (SpawnPowerupOrEnemy == 0 || EnemiesCount >= MaxEnemies)
                {
                    int powerUpToSpawn = Random.Range(0, PowerUpItems.Count);

                    itemFactory.CreateItem("PowerUp", PowerUpItems[powerUpToSpawn], new Vector2(X, Y));
                    roomsFilled.Add(i);
                }
                //spawn enemy
                else
                {
                    switch (LevelNumber)
                    {
                        case 1:
                            itemFactory.CreateItem("Enemy Type 1", Enemies[0], new Vector2(X, Y));
                            roomsFilled.Add(i);
                            break;
                        default:

                            int EnemyToSpawn = Random.Range(0, 2);

                            if (EnemyToSpawn == 0)
                            {
                                itemFactory.CreateItem("Enemy Type 1", Enemies[0], new Vector2(X, Y));
                            }
                            else
                            {
                                itemFactory.CreateItem("Enemy Type 2", Enemies[1], new Vector2(X, Y));
                            }

                            roomsFilled.Add(i);

                            break;
                    }

                    EnemiesCount++;
                }
            }
            itemsGenerated = true;
        }
    }
}