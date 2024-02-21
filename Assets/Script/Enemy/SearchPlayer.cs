using UnityEngine;
using UnityEngine.AI;

public class SearchPlayer : MonoBehaviour
{
    public HealthStation healthStation;
    public Transform _playerTransform;
    public float stopDistance = 7f;
    [SerializeField] private EnemyAi _enemyAi;
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        healthStation = FindObjectOfType<HealthStation>();
        _playerTransform = healthStation.transform;
    }

    private void Update()
    {
        Move();
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerTransform.position;
    }

    private void Move()
    {
        if (_playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(_playerTransform.position, transform.position);
            if (distanceToPlayer >= stopDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_playerTransform.position);
            }
            else
            {
                _agent.isStopped = true;
                _enemyAi._shot?.Invoke();
            }
        }
    }
}