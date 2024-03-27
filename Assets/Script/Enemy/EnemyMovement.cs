using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Target))]
public class EnemyMovement : MonoBehaviour
{
    public Action OnMove;
    private NavMeshAgent _agent;
    private Target _target;
    private bool _canMoving = true;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _target = GetComponent<Target>();
        _target.OnDie += () => _canMoving = false;
    }

    private void FixedUpdate()
    {
        if (Station.Singleton == null || !_canMoving)
        {
            _agent.destination = transform.position;
            return;
        }
        OnMove?.Invoke();
        _agent.destination = Station.Singleton.transform.position;
    }
    public void Initialize()
    {
        _canMoving = true;
        if (_agent != null)
            _agent.isStopped = false;
    }
    public void MoveOn()
    {
        if (_agent != null)
            _agent.isStopped = false;
    }
    public void MoveOff()
    {
        if (_agent != null)
            _agent.isStopped = true;
    }
}
