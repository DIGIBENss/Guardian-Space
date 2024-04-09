using System;
using System.Collections;
using System.Collections.Generic;
//using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    
    public FX Fx { get; private set; }
    public EnemyCout SpaceShip { get; private set; }
    public Action<EnemyHealth> OnDiee;
    [SerializeField] private SearchPlayer _searchPlayer;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float Health => _health;

    [SerializeField] Slider _healthBar;
    [SerializeField, Range(20, 100)] private int _gold = 20;

    private void Start()
    {
        _healthBar.minValue = 0;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _health;
    }

    private void Awake()
    {
        SpaceShip = new EnemyCout();  
        Fx= new FX();
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        if (_health <= 0)
        {
            _health = 0;
            OnDiee?.Invoke(this);
            if (Station.Singleton != null)
            {
                Fx.GetTransform(transform.position);
                Station.Singleton.GetComponent<Wallet>().AddMoney(_gold);
                SpaceShip.UpdateStat();
                Destroy(gameObject);
            }
        }
    }

    public void Initialize()
    {
        _health = _maxHealth;
    }

    public void TakeHeal(float value)
    {
        if (_health + value <= _maxHealth)
        {
            _health += value;
        }
    }


}