using System.Collections;
using UnityEngine;

public class AnimationExplosionManager : MonoBehaviour
{
    public static float animationLength = 0.4f;
    void Start()
    {
        StartCoroutine(DestroyAtEndOfAnimation());
    }

    IEnumerator DestroyAtEndOfAnimation()
    {
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }
}
