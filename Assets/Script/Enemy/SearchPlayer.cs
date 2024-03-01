using UnityEngine;
using UnityEngine.AI;

public class SearchPlayer : MonoBehaviour
{
    public float stopDistance = 7f;
    [SerializeField] private EnemyAi _enemyAi;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Move();
    }

    public Vector3 GetPlayerPosition()
    {
        return Station.Singleton.transform.position;
    }

    private void Move()
    {
        if (Station.Singleton != null)
        {
            float distanceToPlayer = Vector3.Distance(Station.Singleton.transform.position, transform.position);
            if (distanceToPlayer >= stopDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(Station.Singleton.transform.position);
            }
            else 
            {
                _agent.isStopped = true;
                _enemyAi._shot?.Invoke();
            }
        }
    }
}