using UnityEngine;

namespace BehaviorTree
{
    public abstract class BTree : MonoBehaviour
    {
        
        protected Node root = null;

        private void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}
