using System;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected float _lifeTime, _damage;
    [SerializeField] protected float _speed;
    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public float Damage => _damage;

    private void Start()
    {
        _speed = 0.1f;
    }

    private void FixedUpdate() =>
        Move();
  
    protected void Move() =>
        this.transform.Translate(Vector3.forward * _speed);

    protected virtual void DoDamage(IDamageable damageable) =>
        damageable.TakeDamage(_damage);

    public void SetSpeed(float value) => _speed += value;

    public void Create(Vector3 pos, Quaternion rotation, float damage)
    {
        this._damage = damage;
        var bullet = Instantiate(this, pos, rotation);
        Destroy(bullet?.gameObject, _lifeTime);
    }
    
}
