using UnityEngine;
using BehaviorTree;

public class CheckObstacleAhead : Node
{
    private readonly Transform transform;

    public CheckObstacleAhead(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");

        bool notFoundTarget = target == null;
        if (notFoundTarget)
        {
            state = NodeState.Failure;
            return state;
        }

        Transform targetTf = (Transform)target;

        if (targetTf != null && ThereIsNoObstacleAhead(targetTf))
        {
            state = NodeState.Success;
            return state;
        }


        state = NodeState.Failure;
        return state;
    }

    bool ThereIsNoObstacleAhead(Transform targetTf)
    {
        RaycastHit hit;
        Vector3 vectorDirPlayerToTarget = targetTf.position - transform.position;
        Ray ray = new Ray(transform.position, vectorDirPlayerToTarget);

        int ignoreLayerMask = ~LayerMask.GetMask("Default");
        Physics.Raycast(ray, out hit, vectorDirPlayerToTarget.magnitude, ignoreLayerMask);


        bool isObstacleBlockingView = (hit.collider.tag == "Island");
        return !isObstacleBlockingView; 
    }
}
