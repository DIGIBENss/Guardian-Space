using NTC.Pool;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public Action<Target> OnDiee;
    public Action OnDie;
    public float Health => _health;
    public float MaxHealth => _maxHealth;

    [SerializeField]private float _maxHealth = 50;
    [SerializeField, Range(20,100)] private int _gold = 20;
 
    private float _health;
    private bool _canBeDamaged = true;
    private bool _isDead = false;

    private void Start() => _health = _maxHealth;

    
    public void TakeDamage(float damage)
    {
        if (!_canBeDamaged || _isDead) return;
        _canBeDamaged = false;
        _health -= damage;
        
        if(_health <= 0)
        {
            _isDead = true;
            _health = 0;
            OnDie?.Invoke();
            OnDiee?.Invoke(this);
            if (Station.Singleton != null)
            {
                //ZombieCounter.UpdateStat();
                Station.Singleton.GetComponent<Wallet>().AddMoney(_gold);
                NightPool.Despawn(this.gameObject, 2f);
            }
        }
        _canBeDamaged = true;
    }
    public void Initialize()
    {
        _health = _maxHealth;
        _isDead = false;
        _canBeDamaged = true;
    }
    public void TakeHeal(float value)
    {
        if(_health + value <= _maxHealth)_health += value;
    }
}
