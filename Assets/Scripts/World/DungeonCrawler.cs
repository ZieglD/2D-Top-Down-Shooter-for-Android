using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Legt fest, wie sich die Raumerstellung verhält
public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }


    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }


    // Steuert, die Richtung, in die die Räume generiert werden (mit der directionMovementMap von DungeonCrawlerController.cs)
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}
