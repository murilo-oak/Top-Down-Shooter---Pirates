using UnityEngine;

public class PlayerBoundsGenerator : MonoBehaviour
{
    Camera mainCamera;
    
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

        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, zPos));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, zPos));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, zPos));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, zPos));

        float height = (bottomLeft - topLeft).magnitude;
        float width = (bottomLeft - bottomRight).magnitude;

        CreateBoxCollider("LeftBounds", bottomLeft + new Vector3(0, topLeft.y, 0), new Vector3(0.1f, height, 10f)) ;
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
    }
}
