using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using Complete;
using System.Runtime.CompilerServices;

public class TaskGoRandom : Node
{
    private TankBT tankBT;

    private Transform _transform;
    private List<Transform> wayPointList;

    private Transform _currentWaypoint = null;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    public TaskGoRandom(TankBT tankBT, Transform transform)
    {
        this.tankBT = tankBT;
        _transform = transform;
        wayPointList = new List<Transform>();
        Transform wayPoint = GameObject.Find("Waypoint").transform;
        foreach (Transform wp in wayPoint)
        {
            wayPointList.Add(wp);
        }
        _currentWaypoint = GetNearestNode();
    }

    public override NodeState Tick()
    {
        Debug.Log("Go Random");
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else
        {
            Transform wp = _currentWaypoint;
            if (Vector3.Distance(_transform.position, wp.position) < TankBT.hitBoxRadius)
            {
                _waitCounter = 0f;
                _waiting = true;
                _currentWaypoint = GetRandomWaypoint(_currentWaypoint);
            }
            else
            {
                //if (Vector3.Angle(_transform.forward, wp.position - _transform.position) < TankBT.degreeError)
                //{
                //    tankBT.Turn(0);
                //    tankBT.Move(1);
                //}
                //else
                //{
                //    tankBT.Turn(Vector3.Dot(_transform.forward, wp.position - _transform.position));
                //    tankBT.Move(0);
                //}
                tankBT.Turn(Vector3.Dot(_transform.forward, wp.position - _transform.position));
                tankBT.Move(1);
            }
        }
        state = NodeState.RUNNING;
        return state;
    }

    public Transform GetNearestNode()
    {
        Transform res = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < wayPointList.Count; i++)
        {
            if (Vector3.Distance(_transform.position, wayPointList[i].position) < distance)
            {
                res = wayPointList[i];
                distance = Vector3.Distance(_transform.position, wayPointList[i].position);
            }
        }
        return res;
    }

    public Transform GetRandomWaypoint(Transform waypoint)
    {
        Transform[] wpList = waypoint.gameObject.GetComponent<Neighbor>().wayPointList;
        return wpList[Random.Range(0, wpList.Length)];
    }
}
