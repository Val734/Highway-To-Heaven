using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public UnityEvent WinMenu;


    public HealthBar enemyHealthBar; // Asegúrate de asignar la referencia a la barra de salud del enemigo en el Inspector.

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy.GetComponent<EnemyMovement>().Die();
            StartCoroutine(DemonDie());



            // Aquí puedes manejar la lógica cuando el enemigo se queda sin vida.
            Debug.Log("geimm obeeer");

        }
    }
    IEnumerator DemonDie()
    {
        yield return new WaitForSeconds(5f);
        WinMenu.Invoke();

    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    void UpdateHealthBar()
    {
        float fillValue = (float)currentHealth / maxHealth;
        enemyHealthBar.UpdateHealthBar(fillValue);
    }
}
