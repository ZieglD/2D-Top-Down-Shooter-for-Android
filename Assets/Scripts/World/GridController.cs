using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Erstellt ein Grid, welches innerhalb der Wände eines Raumes gelegt wird.
// Diesem Grid können dann an jeder Position Objekte wie Gegner zufällig zugewiesen werden.
public class GridController : MonoBehaviour
{
    public Room room;

    // Grid, serializable, damit man es im Editor ändern kann
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }


    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();


    // Erstellung des Grids, -2 wegen den Wänden, darin sollen keine Gegner erscheinen
    void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 2;
        GenerateGrid();
    }


    // Erstellung des Grids
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for(int y = 0; y < grid.rows; y++)
        {
            for(int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + "Y: " + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }
        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
}
