using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [Range(0.1f, 10f)] public float shipSizeWidth = 1f;
    [Range(0.1f, 10f)] public float shipSizeHeight = 1f;
    private void OnDrawGizmos()
    {
        float halfsizeWidth = shipSizeWidth / 2;
        float halfsizeHeight = shipSizeHeight / 2;
        Gizmos.color = Color.yellow;
        Vector3 position = transform.position;
        Gizmos.DrawLine(position + transform.right * halfsizeWidth + transform.up * halfsizeHeight, position -transform.right * halfsizeWidth + transform.up * halfsizeHeight);
        Gizmos.DrawLine(position + transform.right * halfsizeWidth - transform.up * halfsizeHeight, position - transform.right * halfsizeWidth - transform.up * halfsizeHeight);

    }
}
