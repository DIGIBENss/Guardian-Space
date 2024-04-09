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
    public bool IsCreateStation;

    [SerializeField] private Zona _zona;
    [Header("Weapon")] [SerializeField] private BulletStation _bullet;
    private float _damage = 10, _searchRadius = 9;
    public float FireRate;
    [Header("Health")] [SerializeField] float _maxHealth;
    [SerializeField] private Slider _sliderHP;
    [Header("FX")] [SerializeField] private GameObject _damageOfStationFX;
    [SerializeField] private MenuPause _menu;
 
    public void OnRepair() => InvokeRepeating("Repair", 0, 0.5f);
    
    public void UpgradeFireRate()
    {
        FireRate -= 0.15f;
        MainWeapon.FireRate = FireRate;
    }
    public void UpgradeMainWeaponAndZona()
    {
        MainWeapon.Up();
        _zona.UP();
    }
    public void UpgradeRepair(int value) => Health.SkillRepairLvl(value);

    public void UpgradeEnduranceStation()
    {
        Health.Up();
        _sliderHP.value = _maxHealth;
    }

    private void Start()
    {
        _sliderHP.minValue = 0;
        _sliderHP.maxValue = _maxHealth;
        _sliderHP.value = _maxHealth;
    }

    private void Awake()
    {
        lock (Singleton = this)
            Health = new(_maxHealth);
        Health.OnValueChaned += ChangeSliderValue;
        Health.OnValueMaxSlider += ChangeSliderMaxValue;
        MainWeapon = new(_bullet, _damage, _searchRadius, FireRate, _zona);
        Health.OnDie += () =>
        {
            IsAlive = false;
            GameManager.Instance.CreateDestroyStationFx();
            Destroy(gameObject);
            _menu.Restart();
        };
    }

    private void Update()
    {
        MainWeapon.Shoot(transform);
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
        Gizmos.color = Color.blue;
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, _searchRadius);
        foreach (var collider in colliders)
        {
            Gizmos.DrawLine(transform.position,
                collider.transform.position); 
            Gizmos.DrawWireSphere(collider.transform.position, 0.5f);
        }
    }
    
    private void ChangeSliderValue(float value)
    {
        _sliderHP.value = value;
        if (Health.Health < Health.MaxHealth / 2 && IsCreateStation == false)
        {
            Instantiate(_damageOfStationFX);
            IsCreateStation = true;
        }
    }
    private void ChangeSliderMaxValue(float value) => _sliderHP.maxValue = value;
    private void Repair() => Health.Repair();
    
    private void OnDestroy()
    {
        Health.OnValueChaned -= ChangeSliderValue;
        Health.OnValueMaxSlider -= ChangeSliderMaxValue;
    }
}