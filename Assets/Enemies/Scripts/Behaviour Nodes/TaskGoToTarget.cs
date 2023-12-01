using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TaskGoToTarget : Node
{
    private readonly Transform transform;
    private NavMeshPath pathToTarget;

    private readonly MoveFowardCommand moveFowardCommand;
    private readonly RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    private readonly RotateClockwiseCommand rotateClockwiseCommand;

    public TaskGoToTarget(Transform transform,
                      MoveFowardCommand moveFowardCommand,
                      RotateClockwiseCommand rotateClockwiseCommand,
                      RotateAnticlockwiseCommand rotateAnticlockwiseCommand)
    {
        this.transform = transform;
        this.moveFowardCommand = moveFowardCommand;
        this.rotateAnticlockwiseCommand = rotateAnticlockwiseCommand;
        this.rotateClockwiseCommand = rotateClockwiseCommand;
        pathToTarget = new NavMeshPath();
    }

    public override NodeState Evaluate()
    {
        Transform targetTf = (Transform)GetData("target");

        if (FoundTarget(targetTf) && PathToTargetExists(targetTf))
        {
            MoveAlongPath(pathToTarget.corners[1]);
        }
        
        state = NodeState.Running;
        return state;
    }

    bool PathToTargetExists(Transform targetTf)
    {
        return NavMesh.CalculatePath(transform.position, targetTf.position, NavMesh.AllAreas, pathToTarget);
    }

    bool FoundTarget(object target)
    {
        return target != null;
    }

    void MoveAlongPath(Vector3 nextCorner)
    {
        moveFowardCommand.Execute(transform.gameObject);
        RotateToTarget(nextCorner);
    }

    void RotateToTarget(Vector3 targetPos)
    {

        if (ShouldRotateAntiClockwise(targetPos))
        {
            rotateAnticlockwiseCommand.Execute(transform.gameObject);
        }
        else
        {
            rotateClockwiseCommand.Execute(transform.gameObject);
        }
    }

    bool ShouldRotateAntiClockwise(Vector3 targetPos)
    {
        Vector3 facePointingDirection = transform.right;
        Vector3 TargetDirection = Vector3.Normalize(targetPos - transform.position);

        float det = MathHelper.Determinant(facePointingDirection, TargetDirection);
        
        return det > 0 && Mathf.Abs(det) > 0.01f;
    }
}
