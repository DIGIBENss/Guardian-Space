using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    public static Station Singleton { get; private set; }
    public StationHealth Health { get; private set; }
    public Weapon MainWeapon { get; private set; }
    public bool IsAlive { get; private set; } = true;

    private bool _canShoot = true;

    private readonly float _additionalFireRate = 0.1f;

    [Header("Weapon")] [SerializeField] private BulletStation _bullet;
    [SerializeField] private float _damage, _searchRadius;
    [SerializeField] private float _fireRate;
    [Header("Health")] [SerializeField] private float _maxHealth;

    [SerializeField] private Slider _sliderHP;

    public void Up()
    {
        _fireRate -= _additionalFireRate;
        MainWeapon.Up();
        Health.Up();
    }
    public void UpgradeEnduranceStation() => Health.Up();

    public void UpgradeMainWeapon() => MainWeapon.Up();

    public void Repairs() => Health.Repair();
    private void Start()
    {
        _sliderHP.minValue = 0;
        _sliderHP.maxValue = _maxHealth;
        _sliderHP.value = _maxHealth;
        InvokeRepeating("Repairs", 0, 5);
    }

    private void Awake()
    {
        lock (Singleton = this)
            Health = new(_maxHealth);
        Health.OnValueChaned += ChangeSliderValue;
        Health.OnValueMaxSlider +=ChangeSliderMaxValue;
        MainWeapon = new(_bullet, _damage, _searchRadius);
        Health.OnDie += () =>
        {
            IsAlive = false;
            Destroy(gameObject);
        };
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

    private void ChangeSliderValue(float value) => _sliderHP.value  = value;
    private void ChangeSliderMaxValue(float value) => _sliderHP.maxValue = value;

    private void OnDestroy() 
    {
        Health.OnValueChaned -= ChangeSliderValue;
        Health.OnValueMaxSlider -=ChangeSliderMaxValue;
    }
     
}