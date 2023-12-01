using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private BulletParrametersPlacer cannonPositionsParrametersPlacer;
    [TagSelector] public string targetDamageTag;

    [SerializeField] GameObject bulletExplosionAnimation;

    public void SpawnFrontBullet(float bulletLifeTime, float initialSpeed)
    {
        //Front cannon tip position.
        Vector3 spawnPosition = transform.position + transform.right * cannonPositionsParrametersPlacer.frontCannonBulletStartPositionOffset;

        SpawnBullet(spawnPosition, transform.right, initialSpeed, bulletLifeTime);
    }

    public void SpawnSideBullets(float bulletLifeTime, float initialSpeed, int amountBulletsToShoot)
    {
        Vector3 shipSideDir = gameObject.transform.up;
        Vector3 position = transform.position;

        float halfsizeWidth = cannonPositionsParrametersPlacer.shipSizeWidth/2;
        float shipSizeHeight = cannonPositionsParrametersPlacer.shipSizeHeight/2;

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < amountBulletsToShoot; i++)
            {
                float interpolator = i/(float)(amountBulletsToShoot-1);
                
                //Spawn bullets along Ship side.
                Vector3 spawnPosition = Vector3.Lerp(position + transform.right * halfsizeWidth + transform.up * shipSizeHeight,
                                                     position - transform.right * halfsizeWidth + transform.up * shipSizeHeight, 
                                                     interpolator);

                SpawnBullet(spawnPosition, shipSideDir, initialSpeed, bulletLifeTime);
            }

            UpdateSideDirectionAndPosition(ref shipSideDir, ref position, shipSizeHeight);
        }
    }

    void SpawnBullet(Vector3 spawnPosition, Vector3 direction, float initialSpeed, float bulletLifeTime)
    {
        GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rbBullet = bulletSpawned.GetComponent<Rigidbody>();

        rbBullet.AddForce(direction* initialSpeed, ForceMode.Impulse);
        bulletSpawned.GetComponent<Bullet>().SetTargetTagDamage(targetDamageTag);

        Destroy(bulletSpawned, bulletLifeTime);

        SpawnBulletExplosion(spawnPosition);
    }

    void SpawnBulletExplosion(Vector3 position)
    {
        GameObject newExplosion = Instantiate(bulletExplosionAnimation, position, Quaternion.identity);
        newExplosion.transform.SetParent(transform);
        newExplosion.transform.localScale = 0.5f * Vector3.one;
    }

    private void UpdateSideDirectionAndPosition(ref Vector3 sideDir, ref Vector3 position, float shipHalfSizeHeight)
    {
        sideDir = -sideDir;
        position -= 2 * shipHalfSizeHeight * transform.up;
    }
}
