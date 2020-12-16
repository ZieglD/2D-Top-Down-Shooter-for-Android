using UnityEngine;


// Von ObjectRoomSpawner.cs verwendet, damit kann man im Unity Editor den Gegner und die min/max Anzahl, die im Grid erscheinen kann festlegen
[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject itemToSpawn;
    public int minSpawn;
    public int maxSpawn;
}
