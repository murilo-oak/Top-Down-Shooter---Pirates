using UnityEngine;
using BehaviorTree;
using static UnityEngine.GraphicsBuffer;


public class CheckPlayerInAttackRange : Node
{
    private readonly Transform transform;
    public static int playerMask = 1 << 6;
    private float attackRange = 5f;

    public CheckPlayerInAttackRange(Transform transform, float attackRange)
    {
        this.transform = transform;
        this.attackRange = attackRange;
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


        if (HasTransformComponent(targetTf) && IsInAttackRange(targetTf))
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

    bool HasTransformComponent(Transform targetTf)
    {
        return targetTf != null;
    }

    private bool IsInAttackRange(Transform targetTf)
    {
        return Vector3.Distance(transform.position, targetTf.position) <= attackRange;
    }
}
