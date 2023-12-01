using System.Collections;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    public static float animationLength = 0.4f;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAtEndOfAnimation());
    }

    IEnumerator DestroyAtEndOfAnimation()
    {
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }
}
