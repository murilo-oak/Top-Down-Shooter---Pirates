using UnityEngine;
using BehaviorTree;


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

        bool notFoundTarget = target == null;
        if (notFoundTarget)
        {
            state = NodeState.Failure;
            return state; 
        }

        Transform targetTf = (Transform)target;
        bool isInAttackRange = Vector3.Distance(transform.position, targetTf.position) <= attackRange;

        if (isInAttackRange)
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Failure;
        return state;
    }
}
