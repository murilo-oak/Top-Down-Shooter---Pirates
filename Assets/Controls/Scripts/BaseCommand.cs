using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class BaseCommand : ScriptableObject
    {
        public virtual void CheckInputandExecute(InputAction action, GameObject gameObject = null) { }

        public virtual void CheckInputandFixedExecute(InputAction action, GameObject gameObject = null) { }

        public virtual void PerformCommand(GameObject gameObject) { }

    }
}

