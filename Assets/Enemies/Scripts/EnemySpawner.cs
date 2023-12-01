using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] CanvasInfo frequencySpawn;

    float frequencyToSpawnNewEnemy = 1f;
    public List<GameObject> enemyTypes = new List<GameObject>();
   
    [SerializeField] PlayerBoundsGenerator playerBoundsGenerator;

    [SerializeField] Transform playerTf;
    [Min(0f)][SerializeField] float minSpawnDistanceToPlayer;

    void Start()
    {
        frequencyToSpawnNewEnemy = frequencySpawn.timerLengthSeconds;
        ValidateSpawnFrequency();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        Vector3 point;
        
        if (TryToFindRandomPointInWorldToSpawn(out point))
        {
            InstantiateEnemyAtPosition(point);
        }

        yield return new WaitForSeconds(frequencyToSpawnNewEnemy);
        
        StartCoroutine(SpawnEnemy());
    }

    private void ValidateSpawnFrequency()
    {
        if (frequencyToSpawnNewEnemy <= 0)
        {
            frequencyToSpawnNewEnemy = 1;
        }
    }

    bool TryToFindRandomPointInWorldToSpawn(out Vector3 result)
    {
        int attemps = 30;
        for (int i = 0; i < attemps; i++)
        {
            Vector3 randomPoint = GenerateRandomPoint();
            
            Vector3 spawnPosition;
            
            if (TrySampleNavMesh(randomPoint, out spawnPosition) && canSpawn(randomPoint))
            {
                result = spawnPosition;
                return true;                        
            }
        }

        result = Vector3.zero;
        return false;
    }

    bool TrySampleNavMesh(Vector3 point, out Vector3 hitPosition)
    {
        NavMeshHit navMeshHit;
        bool success = NavMesh.SamplePosition(point, out navMeshHit, 1.0f, NavMesh.AllAreas);

        hitPosition = success ? navMeshHit.position : Vector3.zero;
        return success;
    }

    void InstantiateEnemyAtPosition(Vector3 point)
    {
        Instantiate(enemyTypes[RandomIndexEnemyPrefab()], point, RandomRotationAroundZ());
    }

    int RandomIndexEnemyPrefab()
    {
        float randomIndexFloat = Random.Range(0, enemyTypes.Count);
        int randomIndex = Mathf.FloorToInt(randomIndexFloat);
        
        return randomIndex;
    }
    Vector3 GenerateRandomPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere;

        randomPoint.x *= playerBoundsGenerator.width / 2;
        randomPoint.y *= playerBoundsGenerator.height / 2;

        return randomPoint;
    }

    bool canSpawn(Vector3 randomPoint)
    {
        bool farEnoughFromPlayer = Vector3.Distance(randomPoint, playerTf.position) >= minSpawnDistanceToPlayer;
        return farEnoughFromPlayer && !IsPositionOccupiedByAnyEntity(randomPoint);
    }

    Quaternion RandomRotationAroundZ()
    {
        return Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }


    bool IsPositionOccupiedByAnyEntity(Vector3 position)
    {
        //Try near points around choosen random point to see if place is occupied by some entity.
        int ignorelayerMask = ~LayerMask.GetMask("Untagged");
        
        if (Physics.Raycast(position + Vector3.forward * 0.1f, Vector3.up, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f + Vector3.right * 0.5f, Vector3.up, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f - Vector3.right * 0.5f, Vector3.up, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f + Vector3.up * 0.5f, Vector3.up, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f - Vector3.up * 0.5f, Vector3.up, 1.0f, ignorelayerMask))
        {
            return true;
        }

        return false;
    }
}
