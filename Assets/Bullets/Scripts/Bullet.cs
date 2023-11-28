using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Min(0)]
    public int damage = 1;

    [TagSelector] public string targetDamageTag;

    private void OnTriggerEnter(Collider collision)
    {
        GameObject targetGameObject = collision.gameObject;
        bool canDealDamageOnTarget = targetGameObject.CompareTag(targetDamageTag);
        
        if (canDealDamageOnTarget)
        {
            targetGameObject.GetComponent<HealthManager>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void SetTargetTagDamage(string targetDamageTag)
    {
        this.targetDamageTag = targetDamageTag;
    }
}
