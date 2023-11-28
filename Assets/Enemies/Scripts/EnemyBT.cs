using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Controls;

public class EnemyBT : BTree
{
    [Header("Commands of Behaviour Tree")]
    [SerializeField] private MoveFowardCommand moveFowardCommand;
    [SerializeField] private RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    [SerializeField] private RotateClockwiseCommand rotateClockwiseCommand;

    [Header("Task Patrol Task Config")]
    [Min(0.01f)][SerializeField] private float distancePatrolThreshold;
    [Min(0)][SerializeField] private float waitingTimePatrol;
    public Transform[] waypoints;

    [Header("Check Player In FOV View Config")]
    [Min(0f)]    [SerializeField] private float fovRange = 0.3f;

    [Header("Task Go To Target Config")]
    [Min(0)][SerializeField] float movementTriggerDistance;

    [Header("Check Player In Attack Range Config")]
    [Min(0)][SerializeField] float attackRange = 5f;
    
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
                new CheckPlayerInAttackRange(transform, attackRange),
                new TaskShoot(transform, bulletSpawner, rotateClockwiseCommand, rotateAnticlockwiseCommand),
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
