using UnityEngine;

public class BulletParrametersPlacer : MonoBehaviour
{
    [Header("Bullet Start Position Parameters")]
    [Range(0.1f, 10f)] public float shipSizeWidth = 1f;
    [Range(0.1f, 10f)] public float shipSizeHeight = 1f;
    [Range(-10f, 10f)] public float frontCannonPositionOffset;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float halfsizeWidth = shipSizeWidth / 2;
        float halfsizeHeight = shipSizeHeight / 2;

        Vector3 position = transform.position;

        //Draw Range Positons of bullets.
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(position + transform.right * halfsizeWidth + transform.up * halfsizeHeight, position -transform.right * halfsizeWidth + transform.up * halfsizeHeight);
        Gizmos.DrawLine(position + transform.right * halfsizeWidth - transform.up * halfsizeHeight, position - transform.right * halfsizeWidth - transform.up * halfsizeHeight);

        //Cannon Position
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(position + transform.right * frontCannonPositionOffset, 0.2f * Vector3.one);
    }
#endif
}
