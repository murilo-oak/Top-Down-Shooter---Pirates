using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/RotateClockwiseCommand")]
    public class RotateClockwiseCommand : BaseCommand
    {

        [Range(0, 500f)][SerializeField] private float rotationSpeed = 100f;
        public override void Execute(InputAction action, GameObject gameObject = null)
        {
            
        }
        public override void FixedExecute(InputAction action, GameObject gameObject = null)
        {
            if (action.IsPressed())
            {
                Transform tf = gameObject.transform;
                float rotationAngleZ = rotationSpeed * Time.fixedDeltaTime;
                Quaternion zRotation = Quaternion.AngleAxis(rotationAngleZ, -Vector3.forward);
                
                tf.rotation *= zRotation;
            }
        }
    }
}