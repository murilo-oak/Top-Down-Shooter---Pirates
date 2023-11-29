using UnityEngine;
using BehaviorTree;

public class CheckPlayerInFOVRange : Node
{
    private readonly Transform transform;
    public static int playerMask = 1 << 6;
    private float fovRange;
    
    private Collider[] colliders;
    
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
                //Set target data to tree root, so other leaves can access target info.
                parent.parent.SetData("target", colliders[0].transform);
            }

            state = NodeState.Failure;
            return state;
        }

        state = NodeState.Success;
        return state;
    }

    bool TryToFindTargets()
    {
        colliders = Physics.OverlapSphere(transform.position, fovRange, playerMask);
        bool foundTargets = colliders.Length > 0;

        return foundTargets;
    }
}