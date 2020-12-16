using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Script, das das Verhalten der Gegner festlegt
// Wird von den Enemy und RangedEnemy Prefabs verwendet

// Die verschiedenen Gegnerzustände
public enum EnemyState
{
    Idle,
    Walk,
    Follow,
    Die,
    Attack
};


// Die verschiedenen Gegnertypen (Melee sind die blauen, Ranged sind die roten Dreiecke)
public enum EnemyType
{
    Melee,
    Ranged
};


public class EnemyController : MonoBehaviour
{
    GameObject player;

    public EnemyState currentState = EnemyState.Idle;
    public EnemyType enemyType;

    public float enemyHealth = 5;
    public float range;
    public float speed;
    public float attackRange;
    public float bulletSpeed;
    public float coolDown;

    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;
    public bool notInRoom = false;
    
    public GameObject bulletPrefab;

    private Vector3 randomDir;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        switch(currentState)
        {
            case (EnemyState.Walk):
                Walk();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                Death();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }


        // Je nachdem, ob der Spieler im gleichen Raum oder innerhalb der Reichweite des Gegners ist, werden verschiedene Zustände aktiviert
        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
            {
                currentState = EnemyState.Walk;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                currentState = EnemyState.Attack;
            }
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }


    // Schaut, ob die Distanz zw. Gegner und Spieler kleiner als die Reichweite ist (ob Spieler in Reichweite ist)
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }


    // Coroutine, die zufällig die Richtung des Gegners verändert
    // Quaternions werden in Unity für Rotationen verwendet.
    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 6f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }


    // Walking-Zustand des Gegners.
    void Walk()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }


    // Follow-Zustand des Gegners
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    // Coroutine für einen Cooldown nach einem Angriff
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }


    // Attack-Zustand des Gegners, je nach Typ, verschiedene Angriffe
    void Attack()
    {
        if(!coolDownAttack)
        {
            switch(enemyType)
            {
                case(EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                break;
            }
        }
    }


    // Death-Zustand des Gegners, entfernt den Gegner aus dem Spiel.
    public void Death()
    {
        if (enemyType == EnemyType.Melee)
        {
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene("WinMenu");
        }
    }


    // Funktion zum Lebenspunkteabzug der Gegner
    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Death();
        }
    }
}
