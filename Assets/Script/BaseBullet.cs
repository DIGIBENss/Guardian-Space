using UnityEngine;

public abstract class BaseBullet : MonoBehaviour, IBullet
{
    [SerializeField] protected float _lifeTime, _damage;
    [SerializeField, Range(0.1f, 15)] protected float _speed;
    public float Speed => _speed;
    public float LifeTime => _lifeTime;
    public float Damage => _damage;

    private void FixedUpdate() =>
        Move();

    protected void Move() =>
        this.transform.Translate(Vector3.forward * _speed);

    protected virtual void DoDamage(IDamageable damageable) =>
        damageable.TakeDamage(_damage, _speed);
    
    public void Create(Vector3 pos, Quaternion rotation, float damage)
    {
        this._damage = damage;
        var bullet = Instantiate(this, pos, rotation);
        Destroy(bullet?.gameObject, _lifeTime);
    }
    
}
