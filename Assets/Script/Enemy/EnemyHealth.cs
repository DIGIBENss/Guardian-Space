using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    
    public FX Fx { get; private set; }
    public EnemyCout SpaceShip { get; private set; }
    public Action<EnemyHealth> OnDiee;
    public Action<Transform> OnDie;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float Health => _health;

    [SerializeField] Slider _healthBar;
    [SerializeField, Range(20, 100)] private int _gold = 20;

    private void Awake()
    {
        SpaceShip = new EnemyCout();  
        Fx= new FX();
    }

    private void Update()
    {
        _healthBar.value = _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            //OnDie?.Invoke();
            OnDiee?.Invoke(this);
            if (Station.Singleton != null)
            {
                Fx.GetTransform(transform.position);
                Station.Singleton.GetComponent<Wallet>().AddMoney(_gold);
                SpaceShip.UpdateStat();
                NightPool.Despawn(gameObject, 0.1f);
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