using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class BaseCommand : ScriptableObject
    {
        public virtual void Execute(InputAction action, GameObject gameObject = null) { }

        public virtual void FixedExecute(InputAction action, GameObject gameObject = null) { }
    }
}

