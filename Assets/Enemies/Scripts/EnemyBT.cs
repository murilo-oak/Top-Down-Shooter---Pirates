using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Controls;

public class EnemyBT : BTree
{
    public Transform[] waypoints;

    [Min(0f)]    [SerializeField] private float fovRange = 0.3f;
    [Min(0.01f)] [SerializeField] private float distancePatrolThreshold;
    [Min(0)]     [SerializeField] private float waitingTimePatrol;

    [Min(0)] [SerializeField] float movementTriggerDistance;

    [SerializeField] private MoveFowardCommand moveFowardCommand;
    [SerializeField] private RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    [SerializeField] private RotateClockwiseCommand rotateClockwiseCommand;

    private BulletSpawner bulletSpawner;

    private void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        root = SetupTree();
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInAttackRange(transform),
                new TaskShoot(transform, bulletSpawner,rotateClockwiseCommand, rotateAnticlockwiseCommand),
            }),
            new Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(transform, fovRange),
                new TaskGoToTarget(transform, moveFowardCommand, rotateClockwiseCommand, rotateAnticlockwiseCommand, movementTriggerDistance)
            }),
            new TaskPatrol(transform, waypoints, moveFowardCommand, rotateClockwiseCommand, rotateAnticlockwiseCommand, waitingTimePatrol, distancePatrolThreshold)
        }); 

        return root;
    }
}
