using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Range(1, 100)]
    int totalHealth = 5;
    int currentHealth = 5;
    float healthPercetage = 1f;

    [SerializeField] AnimationExplosionManager deathExplosionAnimation;
    [SerializeField] GameObject healthBarUIPrefab;

    Animator animator;
    GameObject healthBarUI;
    HealthBarColorManager healthBarcolorManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        healthBarUI = Instantiate(healthBarUIPrefab, transform.position, Quaternion.identity);
        healthBarcolorManager = healthBarUI.GetComponent<HealthBarColorManager>();
    }

    private void Update()
    {
        PositionUI();
    }

    private void OnDestroy()
    {
        Destroy(healthBarUI);
    }

    void PositionUI()
    {
        // Set health bar position bellow Game object.
        healthBarUI.transform.position = 1.1f * Vector3.down + transform.position;
    }
    public void TakeDamage(int damagePoints)
    {
        currentHealth = Mathf.Max(0, currentHealth - damagePoints);
        UpdateHealthPercentage();
        CheckHealthAndUpdateSprite();
    }

    public void Heal(int healPoints)
    {
        healPoints = Mathf.Abs(healPoints);
        currentHealth += Mathf.Min(currentHealth + healPoints, totalHealth);
        UpdateHealthPercentage();
    }

    private bool CheckHealthAndUpdateSprite()
    {
        UpdateHealthPercentage();

        if (EntityDied()) {
            Die();
            return true;
        }

        if (HasGameObjectAnimator())
        {
            UpdateHealthVisualGameObject();
            UpdateHealthBarUI();
        }

        return false;
    }

    void UpdateHealthPercentage()
    {
        healthPercetage = currentHealth / (float)totalHealth;
    }
    bool EntityDied()
    {
        return healthPercetage <= 0;
    }


    public void Die()
    {
        GameManager gameManager = GameManager.instance;
        GameStateController gameStateController = gameManager.gameStateController;

        Instantiate(deathExplosionAnimation, transform.position, Quaternion.identity);
        deathExplosionAnimation.transform.localScale = Vector3.one * 3.3f;
        
        Destroy(gameObject);

        if (gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.scoreManager.IncreaseScore();
        }
        else
        {
            StartCoroutine("SleepUntilDeathAnimationEnds");
            gameStateController.ChangeState(gameStateController.gameOverState);
        }
    }

    void UpdateHealthBarUI()
    {
        healthBarcolorManager.SetHealthColorByPercentageHealth(healthPercetage);
    }
    void UpdateHealthVisualGameObject()
    {
        if (HasGameObjectAnimator())
        {
            if (healthPercetage <= 0.3f)
            {
                animator.SetTrigger("onAlmostDying");
                return;
            }

            if (healthPercetage <= 0.75f)
            {
                animator.SetTrigger("onCharacterBelowHalfHealth");
                return;
            }
        }
    }

    private bool HasGameObjectAnimator()
    {
        return animator != null;
    }

    IEnumerator SleepUntilDeathAnimationEnds()
    {
        yield return new WaitForSeconds(AnimationExplosionManager.animationLength + 0.1f);
    }
}
