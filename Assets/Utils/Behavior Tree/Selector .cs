using System.Collections.Generic;

namespace BehaviorTree {
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            foreach (Node child in children)
            {
                NodeState childState = child.Evaluate();

                bool isSelectorSuccessfulOrRunning = (childState == NodeState.Success) || 
                                                     (childState == NodeState.Running);
                
                if (isSelectorSuccessfulOrRunning)
                {
                    state = childState;
                    return state;
                }
            }

            state = NodeState.Failure;
            return state;
        }
    }
}
