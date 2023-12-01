using UnityEngine;
using BehaviorTree;
using Controls;
using Utils;

public class TaskShoot : Node
{
    private readonly Transform transform;
    private Transform lastTargetTf;
    
    private readonly BulletSpawner bulletSpawner;

    private readonly RotateAnticlockwiseCommand rotateAnticlockwiseCommand;
    private readonly RotateClockwiseCommand rotateClockwiseCommand;

    private float attackTime = 1f;
    private float attackCounter = 0f;

    public TaskShoot(Transform transform, BulletSpawner bulletSpawner,
                      RotateClockwiseCommand rotateClockwiseCommand,
                      RotateAnticlockwiseCommand rotateAnticlockwiseCommand)
    {
        this.transform = transform;
        this.bulletSpawner = bulletSpawner;

        this.rotateAnticlockwiseCommand = rotateAnticlockwiseCommand;
        this.rotateClockwiseCommand = rotateClockwiseCommand;
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");

        bool targetExist = target != null;
        
        if (TargetExits(target))
        {
            Transform currentTargetTf = (Transform)GetData("target");

            UpdateLastTargetTransform(currentTargetTf);
            UpdateAttackCounter();

            if (ShouldShoot())
            {
                Shoot();

                bool isTargetAlive = target != null;
                
                if (isTargetAlive)
                {
                    ResetAttackCounter();
                }
            }

            Vector3 pointingDirection = transform.right;
            Vector3 targetDirection = Vector3.Normalize(currentTargetTf.position - transform.position);
            
            float det = MathHelper.Determinant(pointingDirection, targetDirection);
            
            bool isNotFacingTargetEnough = Mathf.Abs(det) > 0.1f;

            if (isNotFacingTargetEnough)
            {
                RotateToTarget(det);
            }
        }

        state = NodeState.Running;
        return state;
    }
    bool TargetExits(object target)
    {
        return target != null;
    }

    void UpdateLastTargetTransform(Transform currentTargetTransform)
    {
        if (currentTargetTransform != lastTargetTf)
        {
            lastTargetTf= currentTargetTransform;
        }
    }

    void UpdateAttackCounter()
    {
        attackCounter += Time.deltaTime;
    }

    bool ShouldShoot()
    {
        return attackCounter >= attackTime;
    }
    
    void Shoot()
    {
        bulletSpawner.SpawnFrontBullet(10f, 1f);
    }

    void ResetAttackCounter()
    {
        attackCounter = 0f;
    }

    void RotateToTarget(float det)
    {
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
