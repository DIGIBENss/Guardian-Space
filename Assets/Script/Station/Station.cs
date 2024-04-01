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
    

    [Header("Weapon")] [SerializeField] private BulletStation _bullet;
    [SerializeField] private float _damage, _searchRadius;
    public float FireRate;
    [Header("Health")] [SerializeField] float _maxHealth;
    [SerializeField] private Slider _sliderHP;
    [Header("FX")] [SerializeField] private GameObject _damageOfStationFX;
    public void OnRepair() => InvokeRepeating("Repair", 0, 0.5f);
    public void UpgradeEnduranceStation() => Health.Up();

    public void UpgradeMainWeapon() => MainWeapon.Up();

    public void UpgradeRepair(int value)
    {
        Health.SkillRepairLvl(value);
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
        MainWeapon = new(_bullet, _damage, _searchRadius);
        Health.OnDie += () =>
        {
            IsAlive = false;
            GameManager.Instance.CreateDestroyStationFx();
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
        yield return new WaitForSeconds(FireRate);
        _canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
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