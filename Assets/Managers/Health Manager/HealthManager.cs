using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager: MonoBehaviour
{    
    [Range(1, 100)]
    int totalHealth = 5;
    int currentHealth = 5;
    float healthPercetage = 1f;

    Animator animator;

    [SerializeField] AnimationExplosionManager deathExplosionAnimation;
    [SerializeField] GameObject healthBarUIPrefab;
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
        healthBarUI.transform.position = 1.1f * Vector3.down + transform.position ;
    }
    public void TakeDamage(int damagePoints)
    { 
        currentHealth = Mathf.Max(0, currentHealth - damagePoints);
        CheckHealthAndUpdateSprite();
    }

    public void Heal(int healPoints)
    {
        healPoints = Mathf.Abs(healPoints);
        currentHealth += Mathf.Min(currentHealth + healPoints, totalHealth);
    }

    private bool CheckHealthAndUpdateSprite()
    {
        healthPercetage = currentHealth / (float)totalHealth;
        bool entityDied = healthPercetage <= 0;
        
        if (entityDied) {
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

    public void Die()
    {
        if(gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.scoreManager.IncreaseScore();
            Instantiate(deathExplosionAnimation, transform.position, Quaternion.identity);
            deathExplosionAnimation.transform.localScale = Vector3.one * 3.3f;
            Destroy(gameObject);
        }
        else
        {
            GameStateController gameStateController = GameManager.instance.gameStateController;
            Instantiate(deathExplosionAnimation, transform.position, Quaternion.identity);
            deathExplosionAnimation.transform.localScale = Vector3.one * 3.3f;
            Destroy(gameObject);

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
