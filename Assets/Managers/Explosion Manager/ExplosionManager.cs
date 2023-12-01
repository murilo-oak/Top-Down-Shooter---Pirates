using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [Min(0)][SerializeField] int explosionDamage;
    private HealthManager healthManager;
    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;
        
        if (collisionObject.CompareTag("Player"))
        {
            HealthManager healthManagerPlayer = collisionObject.GetComponent<HealthManager>();
            healthManagerPlayer.TakeDamage(explosionDamage);
            healthManager.Die();
        }
    }
}
