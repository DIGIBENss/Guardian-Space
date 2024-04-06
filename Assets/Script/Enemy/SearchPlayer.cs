using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class SearchPlayer : MonoBehaviour
{
    public float stopDistance = 7f;
    [SerializeField] private EnemyAi _enemyAi;
    [SerializeField] private EnemyHealth _enemyHealth;
    private NavMeshAgent _agent;
    private bool _isWaste;
    private bool _isproverka;
    private Vector3 fallbackPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
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
            
            if (_enemyHealth.Health <= _enemyHealth.MaxHealth / 2 && !_isproverka)
            {
                Waste();
            }
            else if (distanceToPlayer >= stopDistance)
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
    private void Waste()
    {
        if (!_isWaste)
        {
            fallbackPosition = transform.position + (transform.position - Station.Singleton.transform.position).normalized * 15f; 
            _agent.SetDestination(fallbackPosition);
            _agent.isStopped = false;
            _isWaste = true;
        }
        StartCoroutine(RebootWaste());
    }

    private IEnumerator RebootWaste()
    {
        yield return new WaitForSeconds(5);
        _isproverka = true;
        _isWaste = false;
    }
}