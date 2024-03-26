using System.Collections;
using UnityEngine;

public class Station : MonoBehaviour
{
    public static Station Singleton { get; private set; }
    public StationHealth Health { get; private set; }
    public Weapon MainWeapon { get; private set; }
    public bool IsAlive { get; private set; } = true;

    private bool _canShoot = true;

    private readonly float _additionalFireRate = 0.1f;

    [Header("Weapon")]
    [SerializeField] private BulletStation _bullet;
    [SerializeField] private float _damage, _searchRadius;
    [SerializeField] private float _fireRate;
    [Header("Health")]
    [SerializeField] private float _maxHealth;

    public void Up()
    {
        _fireRate -= _additionalFireRate;
        MainWeapon.Up();
        Health.Up();
    } 

    private void Awake()
    {
        lock (Singleton = this)
        Health = new(_maxHealth);
        MainWeapon = new(_bullet, _damage, _searchRadius);
        Health.OnDie += () => IsAlive = false;
    }

    private void Update()
    {
        if (_canShoot)
        {
            MainWeapon.Shoot(transform);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }
}
