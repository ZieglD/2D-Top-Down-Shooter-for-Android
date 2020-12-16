using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Wird vom GameController in der BasementMain Scene verwendet
// Zum Anzeigen und Abziehen der Lebenspunkte des Spielers
public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static int health = 5;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    public static int Health { get => health; set => health = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public Text healthText;


    // Awake kann mit Singleton verglichen werden
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    // Zeigt Lebenspunkte im User Interface an
    void Update()
    {
        healthText.text = "Health: " + health;
    }


    // Zieht Spieler Lebenspunkte ab, wenn weniger als 0 -> Tod/Game Over
    public static void DamagePlayer(int damage)
    {
        health -= damage;
        if (Health <= 0)
        {
            KillPlayer();
        }
    }


    // Wenn Spieler stirbt, wird GameOverMenu geladen und Leben wieder auf 5 gesetzt
    private static void KillPlayer()
    {
        SceneManager.LoadScene("GameOverMenu");
        health = 5;
    }
}
