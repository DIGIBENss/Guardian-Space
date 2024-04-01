using NTC.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyFabrick : MonoBehaviour
{
    public Action OnLevelEnd;
    [Header("Wave")] [SerializeField] private GameObject _sliderGameObject;
    [SerializeField] private Slider _sliderWaveCout;
    [SerializeField] private TextMeshProUGUI _textWave;
    [SerializeField] private List<EnemyAi> _first, _second, _thrid, _four, _five;
    [SerializeField] private List<GameObject> _spawnPoints;

    [Header("Values : ")] [SerializeField, Range(0, 7)]
    private int _enemyPerSpawn;

    [SerializeField] private int _additionalEnemy;
    [SerializeField, Range(1, 60)] private int _timeToNextWave;
    private int _wave = 0, _tier = 0;
    [SerializeField]private List<EnemyHealth> _spawndEnemy = new();


    private void Start()
    {
        _textWave.text = _wave.ToString();
        _sliderWaveCout.minValue = 0;
        _sliderWaveCout.maxValue = _timeToNextWave;
        StartLevel();
    }

    private void Update()
    {
        if (_sliderGameObject.activeSelf)
        {
            _sliderWaveCout.value -= Time.deltaTime;
        }
    }

    public void StartLevel()
    {
        for (int i = 0; i < _enemyPerSpawn; i++)
        {
            var num = UnityEngine.Random.Range(0, _spawnPoints.Count);
            var point = _spawnPoints[num];
            var enemy = NightPool.Spawn(ChoiseEnemy().gameObject, point.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().Initialize();
//            enemy.GetComponent<EnemyMovement>().Initialize();
            enemy.GetComponent<EnemyHealth>().OnDiee += CheckEnemyCount;
            _spawndEnemy.Add(enemy.GetComponent<EnemyHealth>());
        }
    }

    private void CheckEnemyCount(EnemyHealth enemy)
    {
        _spawndEnemy.Remove(enemy);
        if (_spawndEnemy.Count == 0)
        {
            StartCoroutine(StartNewWave());
        }
    }

    private IEnumerator StartNewWave()
    {
        _wave++;
        _textWave.text = _wave.ToString();
        int time = _timeToNextWave;
        if (_wave % 5 == 0)
        {
            print("tierUP");
            _tier++;
            if (_tier > 4) _tier = 0;
            _timeToNextWave += 30;
        }

        OnLevelEnd?.Invoke();
        _enemyPerSpawn += _additionalEnemy;
        _sliderGameObject.SetActive(true);
        _sliderWaveCout.value = _timeToNextWave;
        yield return new WaitForSeconds(_timeToNextWave);
        _sliderGameObject.SetActive(false);
        _timeToNextWave = time;
        StartLevel();
        yield break;
    }

    private EnemyAi ChoiseEnemy()
    {
        switch (_tier)
        {
            case 0:
                return _first[UnityEngine.Random.Range(0, _first.Count)];
            case 1:
                return _second[UnityEngine.Random.Range(0, _second.Count)];
            case 2:
                return _thrid[UnityEngine.Random.Range(0, _thrid.Count)];
            case 3:
                return _four[UnityEngine.Random.Range(0, _four.Count)];
            case 4:
                return _five[UnityEngine.Random.Range(0, _five.Count)];
            default:
                return null;
        }
    }
}