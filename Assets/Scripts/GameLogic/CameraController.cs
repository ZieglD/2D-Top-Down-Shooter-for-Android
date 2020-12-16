using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Kontrolliert die Kamera und legt fest, wie schnell die Kamera dem Spieler folgt, wenn dieser den Raum wechselt
// Das Script gehört zur MainCamera
public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currRoom;
    public float moveSpeedWhenRoomChange;


    // Awake wird immer aufgerufen, wenn eine Szene geladen wird, die das Script beinhaltet
    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        UpdatePosition();
    }


    // Funktion für die Kamera (Wechseln der Räume)
    void UpdatePosition()
    {
        if(currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
    }


    // Kamera wird auf Raummitte gerichtet
    Vector3 GetCameraTargetPosition()
    {
        if(currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }


    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
