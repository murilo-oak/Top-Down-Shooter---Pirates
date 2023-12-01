using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0)]
    public int damagePoints = 1;

    [TagSelector] public string targetDamageTag;
    GameObject targetGameObject;

    private void OnTriggerEnter(Collider collision)
    {
        targetGameObject = collision.gameObject;

        if (BulletHitIsland())
        {
            Destroy(this.gameObject);
            return;
        }

        if (CanDealDamageOnTarget())
        {
            targetGameObject.GetComponent<HealthManager>().TakeDamage(damagePoints);
            Destroy(this.gameObject);
        }
    }

    private bool BulletHitIsland()
    {
        return targetGameObject.CompareTag("Island");
    }
    private bool CanDealDamageOnTarget()
    {
        return targetGameObject.CompareTag(targetDamageTag); 
    }

    public void SetTargetTagDamage(string targetDamageTag)
    {
        this.targetDamageTag = targetDamageTag;
    }
}
