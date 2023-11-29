using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float timerToSpawnNewEnemy = 1f;
    public List<GameObject> enemyTypes = new List<GameObject>();
    public float range = 10.0f;

    Vector3 randomPoint;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        float randomIndexFloat = Random.Range(0, enemyTypes.Count);
        int randomIndex = Mathf.FloorToInt(randomIndexFloat);

        Vector3 point;
        
        if (RandomPoint(range, out point))
        {
            Instantiate(enemyTypes[randomIndex], point, Quaternion.identity);
        }

        yield return new WaitForSeconds(timerToSpawnNewEnemy);
        StartCoroutine(SpawnEnemy());
    }

    bool RandomPoint(float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = Random.insideUnitSphere;

            randomPoint.x *= GameManager.instance.playerBoundsGenerator.width/2;
            randomPoint.y *= GameManager.instance.playerBoundsGenerator.height/2;
            
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                if(!IsPositionOccupied(randomPoint))
                {
                    result = hit.position;
                    return true;
                }
                
            }
        }
        result = Vector3.zero;
        return false;
    }


    bool IsPositionOccupied(Vector3 position)
    {
        //Try near points around random point to see if place is occupied
        int ignorelayerMask = ~LayerMask.GetMask("Untagged");
        
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.forward * 0.1f, Vector3.up, out hit, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f + Vector3.right * 0.5f, Vector3.up, out hit, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f - Vector3.right * 0.5f, Vector3.up, out hit, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f + Vector3.up * 0.5f, Vector3.up, out hit, 1.0f, ignorelayerMask))
        {
            return true;
        }

        if (Physics.Raycast(position + Vector3.forward * 0.1f - Vector3.up * 0.5f, Vector3.up, out hit, 1.0f, ignorelayerMask))
        {
            return true;
        }

        return false;
    }
}
