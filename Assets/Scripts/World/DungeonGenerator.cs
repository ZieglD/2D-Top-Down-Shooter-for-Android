using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Erstellt die Spielwelt mit den in dungeonGenerationData definierten Parametern
// Wird im RoomController in BasementMain verwendet
public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    private List<Vector2Int> dungeonRooms;


    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }


    // Erstellung der Welt. Startet immer mit dem Startraum, danach werden zufällige Räume generiert
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}
