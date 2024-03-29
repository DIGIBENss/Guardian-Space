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
    
    [SerializeField] private TextMeshProUGUI _adviceText;
    [SerializeField] private TextMeshProUGUI _cout;
    
    [SerializeField] private List<Transform> _influence;
    [SerializeField] private List<Transform> _maneuverability;
    [SerializeField] private List<Transform> _endurance;
    [SerializeField] private List<Transform> _repair;
    [SerializeField] private List<Transform> _damage;

    [SerializeField] private List<GameObject> _skills;

    private string _failPurchase = "Необходимо M-Коинов:";
    private int _influenceLevel = 0;
    private int _maneuverabilityLevel = 0;
    private int _enduranceLevel = 0;
    private int _repairLevel = 0;
    private int _damageLevel = 0;

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
            _influenceLevel++;
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
            _maneuverabilityLevel++;
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
            _enduranceLevel++;
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
            _repairLevel++;
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