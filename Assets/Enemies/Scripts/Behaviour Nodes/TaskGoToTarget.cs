using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;

public class TaskGoToTarget : Node
{
    private readonly Transform transform;

    private readonly MoveFowardCommand moveFowardCommand;
    private readonly RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    private readonly RotateClockwiseCommand rotateClockwiseCommand;
    private readonly float movementTriggerDistance;

    public TaskGoToTarget(Transform transform,
                      MoveFowardCommand moveFowardCommand,
                      RotateClockwiseCommand rotateClockwiseCommand,
                      RotateAnticlockwiseCommand rotateAnticlockwiseCommand,
                      float movementTriggerDistance)
    {
        this.transform = transform;
        this.moveFowardCommand = moveFowardCommand;
        this.rotateAnticlockwiseCommand = rotateAnticlockwiseCommand;
        this.rotateClockwiseCommand = rotateClockwiseCommand;
        this.movementTriggerDistance = movementTriggerDistance;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (ShouldMoveToTarget(target))
        {
            MoveToTarget(target);
        }

        state = NodeState.Running;
        return state;
    }

    private bool ShouldMoveToTarget(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) > movementTriggerDistance;
    }

    void MoveToTarget(Transform target)
    {
        moveFowardCommand.Execute(transform.gameObject);
        RotateToTarget(target);
    }

    void RotateToTarget(Transform target)
    {
        Vector3 pointingDirection = transform.right;
        Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

        float det = MathHelper.Determinant(pointingDirection, targetDirection);
        bool shouldRotateAntiClockWise = det > 0 && Mathf.Abs(det) > 0.01f;

        if (shouldRotateAntiClockWise)
        {
            rotateAnticlockwiseCommand.Execute(transform.gameObject);
        }
        else
        {
            rotateClockwiseCommand.Execute(transform.gameObject);
        }
    }
}
