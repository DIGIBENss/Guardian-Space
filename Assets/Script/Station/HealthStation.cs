using CodeHelper;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthStation : MonoBehaviour, IDamageable
{
    public static Func<bool> Station;
    public static HealthStation Singleton { get; private set; }
    public float MaxHealth => _maxHealth;
    public float Health => _health;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Slider _sliderHP;
    private void Awake() => Singleton = this;

    public void TakeHeal(float value)
    {
        if (_health + value <= _maxHealth)
        {
            _health += value;
        }
    }

    public void TakeDamage(float damage, float speed)
    {
        _health -= damage;
        _sliderHP.value = _health;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Station += CheckStation;
    }

    private void OnDisable()
    {
        Station -= CheckStation;
    }

    private void Start()
    {
        _sliderHP.minValue = 0;
        _sliderHP.maxValue = _maxHealth;
        _sliderHP.value = _health;
    }

    private bool CheckStation()
    {
        return _health > 0;
    }
}