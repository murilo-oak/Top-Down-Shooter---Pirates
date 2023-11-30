using UnityEngine;

public class HealthManager: MonoBehaviour
{
    [Range(1, 100)]
    int totalHealth = 5;
    int currentHealth = 5;
    [SerializeField] GameObject player;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
        float healthPercetage = currentHealth / (float)totalHealth;
        bool entityDied = healthPercetage <= 0;
        
        if (entityDied) {
            Die();
            return true;
        }

        if (hasGameObjectAnimator())
        {
            if (healthPercetage <= 0.3f)
            {
                animator.SetTrigger("onAlmostDying");
                return false;
            }

            if (healthPercetage <= 0.75f)
            {
                animator.SetTrigger("onCharacterBelowHalfHealth");
                return false;
            }
        }
        return false;
    }

    private void Die()
    {
        if(gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.scoreManager.IncreaseScore();
        }
        else
        {
            GameStateController gameStateController = GameManager.instance.gameStateController;
            gameStateController.ChangeState(gameStateController.gameOverState);
        }
 
        Destroy(gameObject);
    }

    private bool hasGameObjectAnimator()
    {
        return animator != null;
    }
}
