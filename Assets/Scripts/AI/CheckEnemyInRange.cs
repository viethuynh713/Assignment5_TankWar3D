using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Complete;

public class CheckEnemyInRange : Node
{
    private Transform _transform;

    public CheckEnemyInRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Tick()
    {
        Debug.Log("Check Enemy In Range");
        Collider[] colliders = Physics.OverlapSphere(_transform.position, TankBT.viewRadius, GameManager.Instance.enemyLayerMask);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (Vector3.Angle(_transform.forward, collider.transform.position - _transform.position) < TankBT.viewAngle
                    && collider.gameObject.GetComponent<TankControl>().team != _transform.gameObject.GetComponent<TankControl>().team)
                {
                    parent.parent.SetData("Enemy", collider.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}
