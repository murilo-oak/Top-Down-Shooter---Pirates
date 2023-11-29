using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    private readonly Transform transform;
    private NavMeshPath pathToTarget;

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
        pathToTarget = new NavMeshPath();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target != null)
        {
            if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, pathToTarget))
            {
                MoveAlongPath(pathToTarget.corners[1]);
            }
        }
        
        state = NodeState.Running;
        return state;
    }

    void MoveAlongPath(Vector3 nextCorner)
    {
        moveFowardCommand.Execute(transform.gameObject);
        RotateToTarget(nextCorner);
    }

    void RotateToTarget(Vector3 targetPos)
    {
        Vector3 facePointingDirection = transform.right;
        Vector3 TargetDirection = Vector3.Normalize(targetPos - transform.position);

        float det = MathHelper.Determinant(facePointingDirection, TargetDirection);
        bool shouldRotateAntiClockwise = det > 0 && Mathf.Abs(det) > 0.01f;

        if (shouldRotateAntiClockwise)
        {
            rotateAnticlockwiseCommand.Execute(transform.gameObject);
        }
        else
        {
            rotateClockwiseCommand.Execute(transform.gameObject);
        }
    }
}
