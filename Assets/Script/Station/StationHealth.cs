using System;
using System.Collections;
using UnityEngine;

public class StationHealth : IDamageable
{
    public Action OnDie;
    public Action<float> OnValueChaned;
    public Action<float> OnValueMaxSlider;
    public float MaxHealth => _maxHealth;
    public float Health => _health;
    private float _health, _maxHealth, _additional;
    private float _repair = 5f;

    public StationHealth(float maxH, float add = 100)
    {
        _maxHealth = maxH;
        _additional = add;
        _health = _maxHealth;
    }

    public void TakeHeal(float value)
    {
        _health += _health + value <= _maxHealth ? value : 0;
        OnValueChaned?.Invoke(_health);
    }
        

    public void Up()
    {
       _maxHealth += _additional;
       _health = _maxHealth;
       OnValueMaxSlider?.Invoke(_maxHealth);
    }
       

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnValueChaned?.Invoke(_health);
        if(_health < 0)OnDie?.Invoke();
    }

    public void Repair()
    {
        if(_maxHealth >= _health)
        { 
           _health += _repair;
           OnValueChaned?.Invoke(_health);
        }
    }

    
}