using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttackEnemy : Node
{
    private TankBT tankBT;

    private Transform _transform;

    public TaskAttackEnemy(TankBT tankBT, Transform transform)
    {
        this.tankBT = tankBT;
        _transform = transform;
    }

    public override NodeState Tick()
    {
        Debug.Log("Attack Enemy");
        Transform enemy = (Transform)GetData("Enemy");

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
        tankBT.Turn(Vector3.Dot(_transform.forward, enemy.position - _transform.position));
        tankBT.Move(1);
        if (Vector3.Distance(_transform.position, enemy.position) < 150f)
        {
            tankBT.Fire(true);
        }
        else
        {
            tankBT.Fire(false);
        }

        state = NodeState.RUNNING;
        return state;
    }
}