using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Complete;

public class TaskTank : Node
{
    public Transform _transform;
    public TaskTank(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Tick()
    {
        _transform.Translate(Vector3.forward * TankBT.speed * Time.deltaTime);
        return NodeState.RUNNING;
    }
}
