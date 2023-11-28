using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private ScaleManager scaleManager;
    [TagSelector][SerializeField] public string targetDamageTag;
    public void SpawnFrontBullet(float bulletLifeTime, float initialSpeed)
    {
        GameObject bulletSpawned = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rbBullet = bulletSpawned.GetComponent<Rigidbody>();

        rbBullet.AddForce(transform.right * initialSpeed, ForceMode.Impulse);
        bulletSpawned.GetComponent<Bullet>().SetTargetTagDamage(targetDamageTag);
        Destroy(bulletSpawned, bulletLifeTime);
    }

    public void SpawnSideBullets(float bulletLifeTime, float initialSpeed, int amountBulletsToShoot)
    {
        Vector3 sideDir = gameObject.transform.up;
        Vector3 position = transform.position;

        float halfsizeWidth = scaleManager.shipSizeWidth/2;
        float shipSizeHeight = scaleManager.shipSizeHeight/2;

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < amountBulletsToShoot; i++)
            {
                float interpolator = i/(float)(amountBulletsToShoot-1);
                
                Vector3 bulletPos = Vector3.Lerp(position + transform.right * halfsizeWidth + transform.up * shipSizeHeight, position - transform.right * halfsizeWidth + transform.up * shipSizeHeight, interpolator);
                
                GameObject bulletSpawned = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
                Rigidbody rbBullet = bulletSpawned.GetComponent<Rigidbody>();
                
                rbBullet.AddForce(sideDir * initialSpeed, ForceMode.Impulse);
                
                Destroy(bulletSpawned, bulletLifeTime);
            }
            sideDir = -sideDir;
            position -= transform.up * shipSizeHeight*2;
        }
    }
}
