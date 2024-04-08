using System;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected float _lifeTime, _damage;
    [SerializeField] protected float  _speed;
    protected static float _defaultSpeed = 0.1f;
    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public float Damage => _damage;
    
    
    private void FixedUpdate() =>
        Move();
  
    protected void Move() =>
        this.transform.Translate(Vector3.forward * _speed);

    protected virtual void DoDamage(IDamageable damageable) =>
        damageable.TakeDamage(_damage);

    public void SetSpeed(float value)
    {
        _speed += value;
        print(_speed.ToString());
    }

    public void Create(Vector3 pos, Vector3 rotation, float damage, float speed)
    {
        this._damage = damage;
        this._speed = speed;
        var bullet = Instantiate(this, pos, Quaternion.LookRotation(rotation));
        Destroy(bullet?.gameObject, _lifeTime);
    }
 
    
}
