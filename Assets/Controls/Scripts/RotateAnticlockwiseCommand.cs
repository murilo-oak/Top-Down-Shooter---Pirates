using UnityEngine;
using UnityEngine.InputSystem;


namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/RotateAnticlockwiseCommand")]
    public class RotateAnticlockwiseCommand : BaseCommand
    {

        [Range(0, 500f)][SerializeField] private float rotationSpeed = 100f;
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
            RotateAnticlockwise(gameObject);
        }

        private void RotateAnticlockwise(GameObject gameObject)
        {
            Transform tf = gameObject.transform;
            float rotationAngleZ = rotationSpeed * Time.fixedDeltaTime;
            
            Quaternion zRotation = Quaternion.AngleAxis(rotationAngleZ, Vector3.forward);

            tf.rotation *= zRotation;
        }
    }
}
