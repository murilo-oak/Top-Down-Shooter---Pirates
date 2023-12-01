using UnityEngine;

public class NonRotatingChild : MonoBehaviour
{
    void Update()
    {
        LockRotation();
    }

    void LockRotation()
    {
        Quaternion localRotation = transform.localRotation;

        localRotation = Quaternion.Euler(localRotation.eulerAngles.x, localRotation.eulerAngles.y, 0f);

        transform.localRotation = localRotation;
    }
}