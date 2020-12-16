using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Wird in allen Raumszenen von den jeweiligen Türen verwendet
// Wird in Room.cs und RoomController.cs verwendet
public class Door : MonoBehaviour
{
    // DoorTypes
    public enum DoorType
    {
        left,
        right,
        top,
        bottom
    }

    public DoorType doorType;

    public GameObject doorCollider;
    public GameObject wallCollider;

    private GameObject player;

    private float widthOffset = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Spieler sollte bei Kollision mit Tür, an jeweilige Seite des nächstens Raumes teleportiert werden
    // Funktioniert noch nicht, evtl. irgendeine Einstellung in Unity vergessen
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch (doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    break;
                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    break;
            }
        }
    }
}
