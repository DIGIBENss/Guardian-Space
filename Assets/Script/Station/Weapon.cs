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
            _bullet.Create(firepoint == null ? instancePosition.position : firepoint.position,
                Quaternion.LookRotation(_target.transform.position), Damage);
    }

    private void FindTarget(Vector3 instancePosition)
    {
        if (_target != null && _target.gameObject.activeSelf)
        {
            return;
        }

        _target = null; // Сбрасываем текущую цель

        foreach (var item in Physics.OverlapSphere(instancePosition, SearchRadius))
        {
            if (item.TryGetComponent(out EnemyHealth enemy) && enemy.gameObject.activeSelf)
            {
                _target = enemy;
                break; // Выходим из цикла после нахождения новой цели
            }
        }
    }

    public void Up()
    {
        Damage += _additionalDamage;
        SearchRadius += _additionalRadius;
    }
}