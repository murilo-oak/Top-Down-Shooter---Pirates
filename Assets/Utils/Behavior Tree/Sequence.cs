using System.Collections.Generic;


namespace BehaviorTree {
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        // Returns Running if any child is running. Returns Failure if any child fails.
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(Node child in children) 
            {
                NodeState childState = child.Evaluate();

                if (childState == NodeState.Failure)
                {
                    state = NodeState.Failure;
                    return state;
                }

                if (childState == NodeState.Running)
                {
                    anyChildIsRunning = true;
                    continue;
                }
            }

            state = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return state;
        }
    }
}
