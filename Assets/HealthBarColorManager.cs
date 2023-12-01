using UnityEngine;

public class HealthBarColorManager : MonoBehaviour
{
    private Material healthBarMaterial;
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        healthBarMaterial = spriteRenderer.material;
    }
    public void SetHealthColorByPercentageHealth(float percentageHealth)
    {
        healthBarMaterial.SetFloat("_Health", percentageHealth);
    }
}
