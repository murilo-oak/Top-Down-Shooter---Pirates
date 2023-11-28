using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/RotateClockwiseCommand")]
    public class RotateClockwiseCommand : BaseCommand
    {

        [Range(0, 500f)][SerializeField] private float rotationSpeed = 100f;
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
            RotateClockwise(gameObject);
        }

        private void RotateClockwise(GameObject gameObject) 
        {
            Transform tf = gameObject.transform;
            float rotationAngleZ = rotationSpeed * Time.deltaTime;
            
            Quaternion zRotation = Quaternion.AngleAxis(rotationAngleZ, -Vector3.forward);

            tf.rotation *= zRotation;
        }
    }
}
