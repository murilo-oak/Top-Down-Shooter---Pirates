using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Windows;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/RotateAnticlockwiseCommand")]
    public class RotateAnticlockwiseCommand : BaseCommand
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
                Quaternion zRotation = Quaternion.AngleAxis(rotationAngleZ, Vector3.forward);
                
                tf.rotation *= zRotation;
            }
        }
    }
}
