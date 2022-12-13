using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Complete;

public class CheckItemInRange : Node
{
    private Transform _transform;

    public CheckItemInRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Tick()
    {
        Debug.Log("Check Item In Range");
        object t = GetData("Item");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, TankBT.viewRadius, GameManager.Instance.itemLayerMask);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (Vector3.Distance(_transform.position, collider.transform.position) > TankBT.hitBoxRadius
                        && Vector3.Angle(_transform.forward, collider.transform.position - _transform.position) < TankBT.viewAngle)
                    {
                        parent.parent.SetData("Item", collider.transform);
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
            }
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
