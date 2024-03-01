using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private Transform[] _postionspawns;
    [SerializeField] private GameObject[] _enemys;
    [SerializeField] private float _time;
    [SerializeField] private float _reapetRateSpawnEnemy;

    private void Start()
    {
        InvokeRepeating("RandomSpawnEnemy", _time, _reapetRateSpawnEnemy);
    }

    private void RandomSpawnEnemy()
    {
        if(Station.Singleton.IsAlive)
        {
            var postion = UnityEngine.Random.Range(0, _postionspawns.Length);
            Instantiate(_enemys[0], _postionspawns[postion].transform.position, Quaternion.identity);
        }    
    }

}
