using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/MoveFowardCommand")]
    public class MoveFowardCommand : BaseCommand
    {
        [SerializeField][Range(0f, 500f)] private float movementSpeed;
        public override void CheckInputandExecute(InputAction action, GameObject gameObject = null)
        {
            
        }
        public override void CheckInputandFixedExecute(InputAction action, GameObject gameObject = null)
        {
            bool canExecute = action.IsPressed();

            if (canExecute)
            {
                Execute(gameObject);
            }
        }

        public override void Execute(GameObject gameObject)
        {
            MoveFoward(gameObject);
        }

        private void MoveFoward(GameObject gameObject)
        {
            Rigidbody gameObjectRb = gameObject.GetComponent<Rigidbody>();
            Vector3 gameObjectFacingDirection = gameObject.transform.right;

            gameObjectRb.AddForce(movementSpeed * Time.deltaTime * gameObjectFacingDirection);
        }
    }
}
