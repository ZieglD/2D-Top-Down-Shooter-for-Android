using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script zum Kontrollieren der Projektile
// Wird von den Bullet und EnemyBullet Prefabs verwendet
public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;
    public Vector3 targetVector;
    private Vector2 lastPos;
    private Vector2 currentPos;
    private Vector2 playerPos;


    // Coroutine, die festlegt, wie lange ein Projektil existiert.
    IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    void Start()
    {
        StartCoroutine(BulletLifeTime());
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
    }


    void Update()
    {
        if (isEnemyBullet)
        {
            currentPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(currentPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = currentPos;
        }
    }


    // Position vom Spieler (in EnemyController zum Schießen von Gegnern benutzt)
    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }


    // Abfrage für bullet collision (Gegner, Spieler, Wände)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !isEnemyBullet)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(1);
            Destroy(gameObject);
        }
        if(collision.tag == "Player" && isEnemyBullet)
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
        if(collision.tag == "WallCollision" || collision.tag == "DoorCollision")
        {
            Destroy(gameObject);
        }
    }
}
