using UnityEngine;

public class HealthManager: MonoBehaviour
{
    [Range(1, 100)]
    int totalHealth = 5;
    int currentHealth = 5;

    public void TakeDamage(int damagePoints)
    { 
        currentHealth = Mathf.Max(0, currentHealth - damagePoints);
        CheckHealth();
    }

    public void Heal(int healPoints)
    {
        healPoints = Mathf.Abs(healPoints);
        currentHealth += Mathf.Min(currentHealth + healPoints, totalHealth);
    }

    private bool CheckHealth()
    {
        bool entityDied = currentHealth <= 0;
        
        if (entityDied) {
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
