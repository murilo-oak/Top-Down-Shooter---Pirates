using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/SideShootCommand")]
    public class SideShootCommand : BaseCommand
    {
        [Range(0, 20)][SerializeField] private float initialSpeed = 1f;
        [Range(0, 20)][SerializeField] private float bulletLifeTime = 5f;
        [Range(0, 20)][SerializeField] private int amountSideBulletsToShoot = 3;
        
        public override void CheckInputandExecute(InputAction action, GameObject gameObject = null)
        {
            bool canExecute = action.WasPressedThisFrame();
            
            if (canExecute) 
            {
                Execute(gameObject);
            }
        }
        public override void CheckInputandFixedExecute(InputAction action, GameObject gameObject = null)
        {
            
        }

        public override void Execute(GameObject gameObject)
        {
            SideShoot(gameObject);
        }

        private void SideShoot(GameObject gameObject)
        {
            gameObject.GetComponent<BulletSpawner>().SpawnSideBullets(bulletLifeTime, initialSpeed, amountSideBulletsToShoot);
        }
    }
}