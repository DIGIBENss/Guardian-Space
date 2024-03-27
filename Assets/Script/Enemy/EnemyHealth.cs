using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    public float Health => _health;

    public Slider healthBar;

    void Update()
    {
        healthBar.value = _health;
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHeal(float value)
    {
        if (_health + value <= _maxHealth)
        {
            _health += value;
        }
    }
}