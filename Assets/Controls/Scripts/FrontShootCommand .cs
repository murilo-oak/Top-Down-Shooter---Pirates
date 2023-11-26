using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/FrontShootCommand")]
    public class FrontShootCommand : BaseCommand
    {
        [Range(0, 20)][SerializeField] private float initialSpeed = 1f;
        [Range(0, 20)][SerializeField] private float bulletLifeTime = 5f;
       
        public override void CheckInputandExecute(InputAction action, GameObject gameObject = null)
        {
            bool canPerformCommand = action.WasPressedThisFrame();
            
            if (canPerformCommand) 
            {
                PerformCommand(gameObject);
            }
        }

        public override void CheckInputandFixedExecute(InputAction action, GameObject gameObject = null)
        {
            
        }

        public override void PerformCommand(GameObject gameObject)
        {
            FrontShoot(gameObject);

        }

        private void FrontShoot(GameObject gameObject)
        {
            gameObject.GetComponent<BulletSpawner>().SpawnFrontBullet(bulletLifeTime, initialSpeed);
        }
    }
}