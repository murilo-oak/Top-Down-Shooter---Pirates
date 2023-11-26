using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/SideShootCommand")]
    public class SideShootCommand : BaseCommand
    {
        [Range(0, 20)][SerializeField] private float initialSpeed = 1f;
        [Range(0, 20)][SerializeField] private float bulletLifeTime = 5f;
        [Range(0, 20)][SerializeField] private int amountBulletsToShoot = 3;
        
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
            SideShoot(gameObject);
        }

        private void SideShoot(GameObject gameObject)
        {
            gameObject.GetComponent<BulletSpawner>().SpawnSideBullets(bulletLifeTime, initialSpeed, amountBulletsToShoot);
        }
    }
}