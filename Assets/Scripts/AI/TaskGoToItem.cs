using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToItem : Node
{
    private TankBT tankBT;

    private Transform _transform;

    public TaskGoToItem(TankBT tankBT, Transform transform)
    {
        this.tankBT = tankBT;
        _transform = transform;
    }

    public override NodeState Tick()
    {
        Debug.Log("Go To Item");
        Transform item = (Transform)GetData("Item");

        //if (Vector3.Angle(_transform.forward, item.position - _transform.position) < TankBT.degreeError)
        //{
        //    tankBT.Turn(0);
        //    tankBT.Move(1);
        //}
        //else
        //{
        //    tankBT.Turn(Vector3.Dot(_transform.forward, item.position - _transform.position));
        //    tankBT.Move(0);
        //}
        //tankBT.Turn(Vector3.Dot(_transform.forward, item.position - _transform.position));
        _transform.LookAt(item);
        tankBT.Move(1);

        state = NodeState.RUNNING;
        return state;
    }
}
