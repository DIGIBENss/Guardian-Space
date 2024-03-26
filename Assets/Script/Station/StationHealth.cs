using System;

public class StationHealth : IDamageable
{
    public Action OnDie;
    public float MaxHealth => _maxHealth;
    public float Health => _health;
    private float _health, _maxHealth, _additional;

    public StationHealth(float maxH, float add = 100)
    {
        _maxHealth = maxH;
        _additional = add;
        _health = _maxHealth;
    }

    public void TakeHeal(float value) =>
        _health += _health + value <= _maxHealth ? value : 0;

    public void Up() =>
        _maxHealth += _additional;

    public void TakeDamage(float damage, float speed)
    {
        _health -= damage;
        if(_health < 0)OnDie?.Invoke();
    }

    
}