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
        Vector3 spawnPosition = transform.position + transform.right * cannonPositionsParrametersPlacer.frontCannonBulletStartPositionOffset;
        GameObject bulletSpawned = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rbBullet = bulletSpawned.GetComponent<Rigidbody>();

        rbBullet.AddForce(transform.right * initialSpeed, ForceMode.Impulse);
        bulletSpawned.GetComponent<Bullet>().SetTargetTagDamage(targetDamageTag);
        
        
        GameObject newExplosion = Instantiate(bulletExplosionAnimation, rbBullet.position, Quaternion.identity);
        newExplosion.transform.SetParent(transform);
        newExplosion.transform.localScale = 0.5f * Vector3.one;
        Destroy(bulletSpawned, bulletLifeTime);
    }

    public void SpawnSideBullets(float bulletLifeTime, float initialSpeed, int amountBulletsToShoot)
    {
        Vector3 sideDir = gameObject.transform.up;
        Vector3 position = transform.position;

        float halfsizeWidth = cannonPositionsParrametersPlacer.shipSizeWidth/2;
        float shipSizeHeight = cannonPositionsParrametersPlacer.shipSizeHeight/2;

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < amountBulletsToShoot; i++)
            {
                float interpolator = i/(float)(amountBulletsToShoot-1);
                
                Vector3 bulletPos = Vector3.Lerp(position + transform.right * halfsizeWidth + transform.up * shipSizeHeight, position - transform.right * halfsizeWidth + transform.up * shipSizeHeight, interpolator);
                
                GameObject bulletSpawned = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
                Rigidbody rbBullet = bulletSpawned.GetComponent<Rigidbody>();
                
                rbBullet.AddForce(sideDir * initialSpeed, ForceMode.Impulse);
                bulletSpawned.GetComponent<Bullet>().SetTargetTagDamage(targetDamageTag);

                GameObject newExplosion = Instantiate(bulletExplosionAnimation, rbBullet.position, Quaternion.identity);
                newExplosion.transform.SetParent(transform);
                newExplosion.transform.localScale = 0.5f * Vector3.one;

                Destroy(bulletSpawned, bulletLifeTime);
            }
            sideDir = -sideDir;
            position -= 2 * shipSizeHeight * transform.up;
        }
    }
}
