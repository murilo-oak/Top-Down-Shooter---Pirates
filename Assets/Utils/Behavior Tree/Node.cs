using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public class Node
    {
        protected NodeState state;
        public Node parent;
        protected List<Node> children = new List<Node>();
        private Dictionary<string, object> dataContext = new Dictionary<string, object>(); 
             
        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children) 
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Failure;


        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            // Checks if the key is present in the local data context
            if (dataContext.TryGetValue(key, out value)) 
            {
                return value;
            }

            // If not present, recursively searches in parent nodes.
            Node node = parent;

            while (node != null)
            {
                value = node.GetData(key);

                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            // Checks if the key is present in the local data context.
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            // If not present, recursively searches in parent nodes.
            Node node = parent;
            
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) 
                {
                    return true;
                }
                node = node.parent;
            }

            return false;
        }
    }
}