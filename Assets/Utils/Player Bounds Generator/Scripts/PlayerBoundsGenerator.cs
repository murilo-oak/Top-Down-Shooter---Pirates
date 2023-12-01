using UnityEngine;

public class PlayerBoundsGenerator : MonoBehaviour
{
    Camera mainCamera;

    public Vector3 bottomLeft { get; private set; }
    public Vector3 bottomRight { get; private set; }
    public Vector3 topLeft { get; private set; }
    public Vector3 topRight { get; private set; }

    public float height { get; private set; } = 0f;
    public float width { get; private set; } = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
        CreateBounds(); 
    }

    void CreateBounds()
    {
        //Player Cannot leave Screen
        CreateBoxColliders();
    }

    void CreateBoxColliders()
    {
        float zPos = Mathf.Abs(mainCamera.transform.position.z);

        bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, zPos));
        bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, zPos));
        topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, zPos));
        topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, zPos));

        height = (bottomLeft - topLeft).magnitude;
        width = (bottomLeft - bottomRight).magnitude;

        CreateBoxCollider("LeftBounds", bottomLeft + new Vector3(0, topLeft.y, 0), new Vector3(0.1f, height, 10f));
        CreateBoxCollider("RightBounds", bottomRight + new Vector3(0, topRight.y, 0), new Vector3(0.1f, height, 10f));
        CreateBoxCollider("TopBounds", topLeft + new Vector3(topRight.x, 0, 0), new Vector3(width, 0.1f, 10f));
        CreateBoxCollider("BottomBounds", bottomLeft + new Vector3(bottomRight.x, 0, 0), new Vector3(width, 0.1f, 10f));
    }

    void CreateBoxCollider(string colliderName, Vector3 position, Vector3 size)
    {
        GameObject colliderObject = new GameObject(colliderName);
        colliderObject.AddComponent<BoxCollider>();

        BoxCollider boxCollider = colliderObject.GetComponent<BoxCollider>();

        boxCollider.center = position;
        boxCollider.size = size;
        boxCollider.gameObject.layer = LayerMask.NameToLayer("ScreenBounds");
    }
}
