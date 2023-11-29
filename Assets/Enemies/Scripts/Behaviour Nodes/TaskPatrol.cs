using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;
using Unity.AI;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using System.IO;
public class TaskPatrol : Node
{
    private readonly Transform transform;
    private readonly Transform[] waypoints;

    private readonly MoveFowardCommand moveFowardCommand;
    private readonly RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    private readonly RotateClockwiseCommand rotateClockwiseCommand;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    private float distancePatrolThreshold;
    private NavMeshPath pathToTarget;

    public TaskPatrol(Transform transform, Transform[] waypoints,
                      MoveFowardCommand moveFowardCommand,
                      RotateClockwiseCommand rotateClockwiseCommand,
                      RotateAnticlockwiseCommand rotateAnticlockwiseCommand,
                      float waitingTimePatrol, float distancePatrolThreshold)
    {
        this.transform = transform;
        this.waypoints = waypoints;
        this.moveFowardCommand = moveFowardCommand;
        this.rotateAnticlockwiseCommand = rotateAnticlockwiseCommand;
        this.rotateClockwiseCommand = rotateClockwiseCommand;
        waitTime = waitingTimePatrol;
        this.distancePatrolThreshold = distancePatrolThreshold; 
        pathToTarget = new NavMeshPath();
    }

    public override NodeState Evaluate()
    {
        UpdatePatrolWaitingState();
        state = NodeState.Running;

        return state;
    }

    void UpdatePatrolWaitingState()
    {
        if (waypoints.Length > 0)
        {
            if (waiting)
            {
                UpdateWaitCounter();
                bool isWaitCounterReached = waitCounter >= waitTime;

                if (isWaitCounterReached)
                {
                    StopWaiting();
                }
                return;
            }
            transform.gameObject.GetComponent<PathVisualizer>().target = waypoints[currentWaypointIndex];
            if (!waiting)
            {
                Transform wp = waypoints[currentWaypointIndex];

                if (ReachedTarget(wp))
                {
                    StartWaiting();
                    AdvanceToNextWaypoint();
                }
                else
                {
                    if (NavMesh.CalculatePath(transform.position, wp.position, NavMesh.AllAreas, pathToTarget))
                    {
                        MoveAlongPath(pathToTarget.corners[1]);
                    }
                }
            }
        }
    }
    void UpdateWaitCounter()
    {
        waitCounter += Time.deltaTime;
    }

    void StartWaiting()
    {
        waitCounter = 0f;
        waiting = true;
    }

    void StopWaiting()
    {
        waiting = false;
    }

    bool ReachedTarget(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) < distancePatrolThreshold;
    }

    void AdvanceToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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
        bool shouldRotateAntiClockwise = det > 0 && Mathf.Abs(det) > 0.02f;

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
