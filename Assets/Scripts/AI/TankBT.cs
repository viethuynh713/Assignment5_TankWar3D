using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class TankBT : BehaviorTree.Tree
{
    public float movement = 0; // -1 back, 0 stop, 1 forward
    public float turn = 0; // -1 left, 0 stop, 1 right
    public float fire = -1; // -1 none, 0 release, 1 hold, 2 press

    public static float viewRadius = 500f;
    public static float hitBoxRadius = 28f;
    public static float viewAngle = 60f;
    public static float degreeError = 20f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInRange(transform),
                new TaskAttackEnemy(this, transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckItemInRange(transform),
                new TaskGoToItem(this, transform)
            }),
            new TaskGoRandom(this, transform)
        });
        
        return root;
    }

    public void Move(int moveType)
    {
        float moveUnit = 1.0f / 3;
        if (moveType > 0)
        {
            movement = movement + moveUnit > 0.5f ? 0.5f : movement + moveUnit;
        }
        else if (moveType < 0)
        {
            movement = movement - moveUnit < -0.5f ? -0.5f : movement - moveUnit;
        }
        else
        {
            if (movement > 0)
            {
                movement = movement - moveUnit < 0 ? 0 : movement - moveUnit;
            }
            else if (movement < 0)
            {
                movement = movement + moveUnit > 0 ? 0 : movement + moveUnit;
            }
        }
    }

    public void Turn(float turnType)
    {
        float turnUnit = 1.0f / 3;
        if (turnType > 0)
        {
            turn = turn + turnUnit > 0.5f ? 0.5f : turn + turnUnit;
        }
        else if (turnType < 0)
        {
            turn = turn - turnUnit < -0.5f ? -0.5f : turn - turnUnit;
        }
        else
        {
            if (turn > 0)
            {
                turn = turn - turnUnit < 0 ? 0 : turn - turnUnit;
            }
            else if (turn < 0)
            {
                turn = turn + turnUnit > 0 ? 0 : turn + turnUnit;
            }
        }
    }

    public void Fire(bool fireType)
    {
        Debug.Log("Fire: " + fireType);
        if (fireType)
        {
            if (fire <= 0)
            {
                fire = 2;
            }
            else
            {
                fire = 1;
            }
        }
        else
        {
            if (fire >= 1)
            {
                fire = 0;
            }
            else
            {
                fire = -1;
            }
        }
    }
}
