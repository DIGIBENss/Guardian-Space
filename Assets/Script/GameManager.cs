using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _destroyEnemyPrefab;
    [SerializeField] private GameObject _destroyStation;
    public TextMeshProUGUI RankText;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateFX(Vector3 position)
    {
        position.y = Random.Range(1.0f, 2.0f);
        Instantiate(_destroyEnemyPrefab, position, Quaternion.identity);
    }

    public void CreateDestroyStationFx()
    {
        Instantiate(_destroyStation);
    }
    
}
