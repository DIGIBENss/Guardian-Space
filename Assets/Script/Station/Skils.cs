using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skils : MonoBehaviour
{
    [SerializeField] private List<Transform> _influence;
    [SerializeField] private List<Transform> _maneuverability;
    [SerializeField] private List<Transform> _endurance;
    [SerializeField] private List<Transform> _repair;
    [SerializeField] private List<Transform> _damage;

    [SerializeField] private List<GameObject> _skills;

    private int _influenceLevel = -1;
    private int _maneuverabilityLevel = -1;
    private int _enduranceLevel = -1;
    private int _repairLevel = -1;
    private int _damageLevel = -1;
    
    private void Start()
    {
       
        
    }

    public void UpgradeInfluence()
    {
        _influenceLevel++;
        Instantiate(_skills[_influenceLevel], _influence[_influenceLevel]);    
    }
    public void UpgradeManeuverability()
    {
        _maneuverabilityLevel++;
        Instantiate(_skills[_maneuverabilityLevel], _maneuverability[_maneuverabilityLevel]);    
    }
    public void UpgradeEndurance()
    {
        _enduranceLevel++;
        Instantiate(_skills[_enduranceLevel], _endurance[_enduranceLevel]);    
    }
    public void UpgradeRepair()
    {
        _repairLevel++;
        Instantiate(_skills[_repairLevel], _repair[_repairLevel]);    
    }

    public void UpgradeDamage()
    {
        _damageLevel++;
        Instantiate(_skills[_damageLevel], _damage[_damageLevel]);    
    }
}
