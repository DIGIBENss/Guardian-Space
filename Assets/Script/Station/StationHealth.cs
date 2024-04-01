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
    public float Repairs;

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
        if (_health < 0) OnDie?.Invoke();
    }

    public void Repair()
    {
        if (_maxHealth >= _health)
        {
            _health += Repairs;
            OnValueChaned?.Invoke(_health);
        }
    }

    public void SkillRepairLvl(int value)
    {
        switch (value)
        {
            case 0:
                Repairs = 0.5f;
                break;
            case 1:
                Repairs = 1f;
                break;
            case 2:
                Repairs = 2f;
                break;
            case 3:
                Repairs = 2.5f;
                break;
            case 4:
                Repairs = 3f;
                break;
            case 5:
                Repairs = 3.5f;
                break;
            case 6:
                Repairs = 4f;
                break;
        }
    }
}