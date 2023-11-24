using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/FrontShootCommand")]
    public class FrontShootCommand : BaseCommand
    {
        [Range(0, 20)][SerializeField] private float initialSpeed = 1f;
        [Range(0, 20)][SerializeField] private float bulletLifeTime = 5f;
        public override void Execute(InputAction action, GameObject gameObject = null)
        {
            if (action.WasPressedThisFrame()) 
            {
                gameObject.GetComponent<BulletSpawner>().SpawnFrontBullet(bulletLifeTime, initialSpeed);
            }
        }
        public override void FixedExecute(InputAction action, GameObject gameObject = null)
        {
            
        }
    }
}