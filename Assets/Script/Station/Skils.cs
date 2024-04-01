using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Skils : MonoBehaviour
{
    public PriceSkills Price { get; private set; }
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Station _station;
    [SerializeField] private BulletStation _bulletStation;
    
    [Header("Text")][SerializeField] private TextMeshProUGUI _adviceText;
    [SerializeField] private TextMeshProUGUI _cout;
    [SerializeField] private List<TextMeshProUGUI> _textSkills;
    
    [Header("Label")][SerializeField] private List<Transform> _influence;
    [SerializeField] private List<Transform> _maneuverability;
    [SerializeField] private List<Transform> _endurance;
    [SerializeField] private List<Transform> _repair;
    [SerializeField] private List<Transform> _damage;
    
    [Header("Skills")][SerializeField] private List<GameObject> _skills;

    private string _failPurchase = "Необходимо M-Коинов:";
    private int _influenceLevel = 0;
    private int _maneuverabilityLevel = 0;
    private int _enduranceLevel = 0;
    private int _repairLevel = 0;
    private int _damageLevel = 0;
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
        if (Price.PriceInfluence[_influenceLevel] <= _wallet.Money)
        {
            _wallet.SpendMoney(Price.PriceInfluence[_influenceLevel]);
            Instantiate(_skills[_influenceLevel], _influence[_influenceLevel]);
            //скорость между выстрелами 
            _station.FireRate -= 0.15f;
            _influenceLevel++;
            _textSkills[0].text =  _station.FireRate.ToString();
        }
        else
        {
            StartCoroutine(Advice(Price.PriceDamage[_influenceLevel]));
        }
    }

    public void UpgradeManeuverability()
    {
        if (Price.PriceManeuverability[_maneuverabilityLevel] <= _wallet.Money)
        {
            _wallet.SpendMoney(Price.PriceManeuverability[_maneuverabilityLevel]);
            Instantiate(_skills[_maneuverabilityLevel], _maneuverability[_maneuverabilityLevel]);
            _bulletStation.SetSpeed(0.1f);
            _maneuverabilityLevel++;
            // скорость пули
            _textSkills[1].text = _bulletStation.Speed.ToString();
        }
        else
        {
            StartCoroutine(Advice(Price.PriceDamage[_maneuverabilityLevel]));
        }
    }

    public void UpgradeEndurance()
    {
        if (Price.PriceEndurance[_enduranceLevel] <= _wallet.Money)
        {
            _wallet.SpendMoney(Price.PriceEndurance[_enduranceLevel]);
            Instantiate(_skills[_enduranceLevel], _endurance[_enduranceLevel]);
            _station.UpgradeEnduranceStation();
            _enduranceLevel++;
            _textSkills[2].text = _station.Health.MaxHealth.ToString();
        }
        else
        {
            StartCoroutine(Advice(Price.PriceDamage[_enduranceLevel]));
        }
    }

    public void UpgradeRepair()
    {
        if (Price.PriceRepair[_repairLevel] <= _wallet.Money)
        {
            _wallet.SpendMoney(Price.PriceRepair[_repairLevel]);
            Instantiate(_skills[_repairLevel], _repair[_enduranceLevel]);
            _station.UpgradeRepair(_repairLevel);
            _repairLevel++;
            if (_isRepair == false)
            {
                _station.OnRepair();
                _isRepair = true;
            }
            _textSkills[3].text = _station.Health.Repairs.ToString();
        }
        else
        {
            StartCoroutine(Advice(Price.PriceDamage[_repairLevel]));
        }
    }

    public void UpgradeDamage()
    {
        if (Price.PriceDamage[_damageLevel] <= _wallet.Money)
        {
            _wallet.SpendMoney(Price.PriceDamage[_damageLevel]);
            Instantiate(_skills[_damageLevel], _damage[_enduranceLevel]);
            _station.UpgradeMainWeapon();
            _textSkills[4].text = _station.MainWeapon.Damage.ToString();
            _textSkills[5].text = _station.MainWeapon.SearchRadius.ToString();
            _damageLevel++;
        }
        else
        {
            StartCoroutine(Advice(Price.PriceDamage[_damageLevel]));
        }
    }
    private IEnumerator Advice(int value)
    {
        _adviceText.text = _failPurchase;
        _cout.text = value.ToString();
        yield return new WaitForSeconds(1f);
        _adviceText.text = "";
        _cout.text = "";

    }
}