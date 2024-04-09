using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public PriceSkills Price { get; private set; }
    [Header("Links")][SerializeField] private Wallet _wallet;
    [SerializeField] private Station _station;
    [SerializeField] private BulletStation _bulletStation;
    [SerializeField] private UiSkills _uiskills;
    [SerializeField] private Zona _zona;
    
    [Header("Text")] [SerializeField] private TextMeshProUGUI _adviceText;
    [SerializeField] private List<TextMeshProUGUI> _textSkills;

    [Header("Label")] [SerializeField] private List<Transform> _influence;
    [SerializeField] private List<Transform> _maneuverability;
    [SerializeField] private List<Transform> _endurance;
    [SerializeField] private List<Transform> _repair;
    [SerializeField] private List<Transform> _damage;

    [Header("Skills")] [SerializeField] private List<GameObject> _skills;

    private string _failPurchase = "Необходимо M-Коинов:";
    private string _upgradeMax = "Максимальное";
    public int Ratefire = 0;
    public int BulletSpeed = 0;
    public int EnduranceLevel = 0;
    public int RepairLevel = 0;
    public int DamageLevel = 0;
    private bool _isRepair;

    private void Awake()
    {
        Price = new PriceSkills();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _wallet.Money += 100;
        }
    }

    public void UpgradeInfluence()
    {
        if (Ratefire < 7)
        {
            if (Price.PriceRateFire[Ratefire] <= _wallet.Money)
            {
                _wallet.SpendMoney(Price.PriceRateFire[Ratefire]);
                Instantiate(_skills[Ratefire], _influence[Ratefire]);
                //скорость между выстрелами 
                _station.UpgradeFireRate();
                Ratefire++;
                _textSkills[0].text = _station.FireRate.ToString();
            }
        }
        else
        {
            StartCoroutine(UpgradeMaxText());
        }

        _uiskills.CheckAvailableSkills();
    }

    public void UpgradeManeuverability()
    {
        if (BulletSpeed < 7)
        {
            if (Price.PriceBulletSpeed[BulletSpeed] <= _wallet.Money)
            {
                _wallet.SpendMoney(Price.PriceBulletSpeed[BulletSpeed]);
                Instantiate(_skills[BulletSpeed], _maneuverability[BulletSpeed]);
                _station.MainWeapon.UpBulletSpeed(0.1f);
                BulletSpeed++;
                // скорость пули
                _textSkills[1].text = _station.MainWeapon.BulletSpeed.ToString();
            }
        }
        else
        {
            StartCoroutine(UpgradeMaxText());
        }

        _uiskills.CheckAvailableSkills();
    }

    public void UpgradeEndurance()
    {
        if (EnduranceLevel < 7)
        {
            if (Price.PriceEndurance[EnduranceLevel] <= _wallet.Money)
            {
                _wallet.SpendMoney(Price.PriceEndurance[EnduranceLevel]);
                Instantiate(_skills[EnduranceLevel], _endurance[EnduranceLevel]);
                _station.UpgradeEnduranceStation();
                EnduranceLevel++;
                _textSkills[2].text = _station.Health.MaxHealth.ToString();
            }
        }
        else
        {
            StartCoroutine(UpgradeMaxText());
        }

        _uiskills.CheckAvailableSkills();
    }

    public void UpgradeRepair()
    {
        if (RepairLevel < 7)
        {
            if (Price.PriceRepair[RepairLevel] <= _wallet.Money)
            {
                _wallet.SpendMoney(Price.PriceRepair[RepairLevel]);
                Instantiate(_skills[RepairLevel], _repair[RepairLevel]);
                _station.UpgradeRepair(RepairLevel);
                RepairLevel++;
                if (_isRepair == false)
                {
                    _station.OnRepair();
                    _isRepair = true;
                }

                _textSkills[3].text = _station.Health.Repairs.ToString();
            }
        }
        else
        {
            StartCoroutine(UpgradeMaxText());
        }

        _uiskills.CheckAvailableSkills();
    }

    public void UpgradeDamage()
    {
        if (DamageLevel < 7)
        {
            if (Price.PriceDamage[DamageLevel] <= _wallet.Money)
            {
                _wallet.SpendMoney(Price.PriceDamage[DamageLevel]);
                Instantiate(_skills[DamageLevel], _damage[DamageLevel]);
                _station.UpgradeMainWeaponAndZona();
                _textSkills[4].text = _station.MainWeapon.Damage.ToString();
                _textSkills[5].text = _zona.Box.size.x.ToString();
                DamageLevel++;
            }
        }
        else
        {
            StartCoroutine(UpgradeMaxText());
        }

        _uiskills.CheckAvailableSkills();
    }

    private IEnumerator UpgradeMaxText()
    {
        _adviceText.text = _upgradeMax;
        yield return new WaitForSeconds(0.1f);
        _adviceText.text = "";
    }
}