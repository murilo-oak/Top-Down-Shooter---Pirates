using UnityEngine;
using BehaviorTree;

public class CheckNoObstacleAhead : Node
{
    private readonly Transform transform;

    public CheckNoObstacleAhead(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");

        if (!FoundTarget(target))
        {
            state = NodeState.Failure;
            return state;
        }

        Transform targetTf = (Transform)target;

        if (ThereIsNoObstacleAhead(targetTf))
        {
            state = NodeState.Success;
            return state;
        }


        state = NodeState.Failure;
        return state;
    }

    bool FoundTarget(object target)
    {
        return target != null;
    }
    bool ThereIsNoObstacleAhead(Transform targetTf)
    {
        RaycastHit hit;
        Vector3 vectorDirPlayerToTarget = targetTf.position - transform.position;
        Ray ray = new Ray(transform.position, vectorDirPlayerToTarget);

        int ignoreLayerMask = ~LayerMask.GetMask("Default");
        Physics.Raycast(ray, out hit, vectorDirPlayerToTarget.magnitude, ignoreLayerMask);


        bool isObstacleBlockingView = hit.collider.CompareTag("Island");
        return !isObstacleBlockingView; 
    }
}
