using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;

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
    }

    public override NodeState Evaluate()
    {
        UpdatePatrolWaitingState();
        state = NodeState.Running;

        return state;
    }

    void UpdatePatrolWaitingState()
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

        if(!waiting)
        {
            Transform wp = waypoints[currentWaypointIndex];
            bool reachedTarget = Vector3.Distance(transform.position, wp.position) < distancePatrolThreshold;

            if (reachedTarget)
            {
                AdvanceToNextWaypoint();
                StartWaiting();
            }
            else
            {
                MoveToWayPoint(wp);
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

    void AdvanceToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void MoveToWayPoint(Transform wp)
    {
        moveFowardCommand.Execute(transform.gameObject);
        RotateToTarget(wp);
    }

    void RotateToTarget(Transform target)
    {
        Vector3 facePointingDirection = transform.right;
        Vector3 TargetDirection = Vector3.Normalize(target.transform.position - transform.position);
        
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
