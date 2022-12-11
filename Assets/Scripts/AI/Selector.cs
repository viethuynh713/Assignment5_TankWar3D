using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        public override NodeState Tick()
        {
            foreach (Node child in children)
            {
                NodeState childStatus = child.Tick();
                if (childStatus == NodeState.SUCCESS || childStatus == NodeState.RUNNING)
                {
                    return childStatus;
                }
            }
            return NodeState.FAILURE;
        }
    }
}
