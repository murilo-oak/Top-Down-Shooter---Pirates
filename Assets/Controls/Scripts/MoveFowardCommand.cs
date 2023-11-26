using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/MoveFowardCommand")]
    public class MoveFowardCommand : BaseCommand
    {
        [SerializeField][Range(1f, 4f)] private float movementSpeed;
        public override void CheckInputandExecute(InputAction action, GameObject gameObject = null)
        {
            
        }
        public override void CheckInputandFixedExecute(InputAction action, GameObject gameObject = null)
        {
            bool canPerformCommand = action.IsPressed();

            if (canPerformCommand)
            {
                PerformCommand(gameObject);
            }
        }

        public override void PerformCommand(GameObject gameObject)
        {
            MoveFoward(gameObject);
        }

        private void MoveFoward(GameObject gameObject)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * movementSpeed);
        }
    }
}
