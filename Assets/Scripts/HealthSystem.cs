using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour, IHealthSystem
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int CurrenHealth { set => currentHealth = value; }
    public bool player = false;
    public bool modoDios = false;
    private FloatingHealthBar healthBar;
    public Enemy enemy;

    void Start()
    {
        modoDios = false;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        if (GetComponent<Enemy>() != null)
        {
            enemy = GetComponent<Enemy>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            modoDios = true;
        }

        if (modoDios && player)
        {
            currentHealth = maxHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;


        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        /* if (player)
         {
             vidaUI[currentHealth].SetActive(false);
             Debug.Log(currentHealth);
         }*/

        if (currentHealth <= 0)
        {
            if (player)
            {
                SceneManager.LoadScene("GameOverMenu");
            }
            /*else if (boss)
            {
                //explosiones
                Destroy(gameObject);
            }*/
            else
            {
                enemy.Dead();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (player && other.gameObject.CompareTag("Life"))
        {
            Debug.Log("curar");
            if (currentHealth < 200)
            {
                currentHealth = currentHealth + 50;
                if (currentHealth > 200)
                {
                    currentHealth = 200;
                }
                healthBar.UpdateHealthBar(currentHealth, maxHealth);
                Destroy(other.gameObject);
            }
        }
    }
}
