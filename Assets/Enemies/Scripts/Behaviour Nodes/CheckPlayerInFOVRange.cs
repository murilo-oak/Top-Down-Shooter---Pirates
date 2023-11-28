using UnityEngine;
using BehaviorTree;

public class CheckPlayerInFOVRange : Node
{
    private readonly Transform transform;
    private const int playerMask = 1 << 6;
    private float fovRange;

    Collider[] colliders;

    public CheckPlayerInFOVRange(Transform transform, float fovRange)
    {
        this.transform = transform;
        this.fovRange = fovRange;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        bool notFoundTarget = target == null;

        if (notFoundTarget)
        {
            if (TryToFindTargets())
            {
                Collider firstTargetCollider = colliders[0];

                //Set target data to tree root, so other leaves can access target info.
                parent.parent.SetData("target", firstTargetCollider.transform);
                
                state = NodeState.Success;
                return state;
            }
        }

        state = NodeState.Failure;
        return state;
    }

    bool TryToFindTargets()
    {
        colliders = Physics.OverlapSphere(transform.position, fovRange, playerMask);
        bool foundTargets = colliders.Length > 0;

        return foundTargets;
    }
}
