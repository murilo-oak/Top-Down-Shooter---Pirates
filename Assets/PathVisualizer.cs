using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    public Transform target;
    NavMeshPath path;
    private void Start()
    {
        path = new NavMeshPath();
        target = transform;
        FindAndVisualizePath();
    }

    private void OnDrawGizmos()
    {
        if(target != null)
        {
            FindAndVisualizePath();
        }
    }

    private void FindAndVisualizePath()
    {
        if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
        {
            DrawPath(path);
        }
    }

    private void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.blue);
        }
    }
}
