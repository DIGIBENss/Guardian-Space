using UnityEngine;

public class Weapon
{
    private float _damage = 50, _searchRadius;
    private BulletStation _bullet;
    private EnemyHealth _target = null;
    private bool _canShoot = true;
    private readonly float _additionalDamage = 5, _additionalRadius = 10;

    public Weapon(BulletStation bullet, float damage, float radius)
    {
        _bullet = bullet;
        _damage = damage;
        _searchRadius = radius;
    }

    public void Shoot(Transform instancePosition, Transform firepoint = null)
    {
        FindTarget(instancePosition.position);
        if (_target != null && _canShoot) _bullet.Create(firepoint == null ? instancePosition.position : firepoint.position, Quaternion.LookRotation(_target.transform.position), _damage);
        
    }

    private void FindTarget(Vector3 instancePosition)
    {
        if (_target != null) return;
        foreach(var item in Physics.OverlapSphere(instancePosition, _searchRadius))
        {
            if(item.TryGetComponent(out EnemyHealth enemy)) _target = enemy;
        }
    }

    public void Up()
    {
        _damage += _additionalDamage;
        _searchRadius += _additionalRadius;
    }
}