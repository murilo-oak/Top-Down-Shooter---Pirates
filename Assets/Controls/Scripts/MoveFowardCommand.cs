using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls {

    [CreateAssetMenu(menuName = "Controls/Commands/MoveFowardCommand")]
    public class MoveFowardCommand : BaseCommand
    {
        [SerializeField][Range(1f, 4f)] private float movementSpeed;
        public override void Execute(InputAction action, GameObject gameObject = null)
        {
            
        }
        public override void FixedExecute(InputAction action, GameObject gameObject = null)
        {
            if (action.IsPressed())
            {
                MoveFoward(gameObject);
            }
        }

        public void MoveFoward(GameObject gameObject)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * movementSpeed);
        }
    }
}
