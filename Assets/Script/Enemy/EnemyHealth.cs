using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public Action<EnemyHealth> OnDiee;
    public Action OnDie;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float Health => _health;

    [SerializeField] Slider healthBar;
    [SerializeField, Range(20, 100)] private int _gold = 20;
    private bool _canBeDamaged = true;
    private bool _isDead = false;
    void Update()
    {
        healthBar.value = _health;
    }


    public void TakeDamage(float damage)
    {
        //if (!_canBeDamaged || _isDead) return;
       // _canBeDamaged = false;
        _health -= damage;
        if (_health <= 0)
        {
            //_health = 0;
            //_isDead = true;
            OnDie?.Invoke();
            OnDiee?.Invoke(this);
            if (Station.Singleton != null)
            {
                //ZombieCounter.UpdateStat();
                Station.Singleton.GetComponent<Wallet>().AddMoney(_gold);
                NightPool.Despawn(this.gameObject, 1f);
            }
        }
       // _canBeDamaged = true;
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