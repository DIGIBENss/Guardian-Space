using UnityEngine;

public class Weapon
{
    public float Damage = 50, SearchRadius;
    private BulletStation _bullet;
    private EnemyHealth _target = null;
    private bool _canShoot = true;
    private readonly float _additionalDamage = 5, _additionalRadius = 3;

    public Weapon(BulletStation bullet, float damage, float radius)
    {
        _bullet = bullet;
        Damage = damage;
        SearchRadius = radius;
    }

    public void Shoot(Transform instancePosition, Transform firepoint = null)
    {
        FindTarget(instancePosition.position);
        if (_target != null && _canShoot)
        {
            Vector3 direction = _target.transform.position - (firepoint == null ? instancePosition.position : firepoint.position);
            if (direction.magnitude > SearchRadius) 
            {
                _target = null; 
            }
            else
            {
                _bullet.Create(firepoint == null ? instancePosition.position : firepoint.position, direction.normalized, Damage);
            }
        }
    }

    private void FindTarget(Vector3 instancePosition)
    {
        if (_target != null && _target.gameObject.activeSelf)
        {
            return;
        }

        _target = null; 

        foreach (var item in Physics.OverlapSphere(instancePosition, SearchRadius))
        {
            if (item.TryGetComponent(out EnemyHealth enemy) && enemy.gameObject.activeSelf)
            {
                _target = enemy;
                break;
            }
        }
    }

    public void Up()
    {
        Damage += _additionalDamage;
        SearchRadius += _additionalRadius;
    }
}