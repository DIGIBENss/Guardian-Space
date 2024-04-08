using System.Collections;
using UnityEngine;

public class Weapon
{
    public float Damage, SearchRadius, FireRate;
    private BulletStation _bullet;
    private EnemyHealth _target;
    private bool _canShoot = true;
    private readonly float _additionalDamage = 5, _additionalRadius = 3;
    private Vector3 _instancePosition;
    private Zona _zona;

    public Weapon(BulletStation bullet, float damage, float radius, float firerate, Zona zona)
    {
        _bullet = bullet;
        Damage = damage;
        SearchRadius = radius;
        FireRate = firerate;
        _zona = zona;
    }

    public void Shoot(Transform instancePosition, Transform firepoint = null)
    {
        if (!_canShoot || _zona.Enemys.Count <= 0 || _zona.Enemys[0] == null) return;
        Vector3 direction = _zona.Enemys[0].transform.position -
                            (firepoint == null ? instancePosition.position : firepoint.position);

        _bullet.Create(firepoint == null ? instancePosition.position : firepoint.position, direction.normalized,
            Damage, 0.2f);
        Station.Singleton.StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(FireRate);
        _canShoot = true;
    }

    public void Up()
    {
        Damage += _additionalDamage;
    }
}