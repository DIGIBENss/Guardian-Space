using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSkills : MonoBehaviour
{
    public PriceSkills Price { get; private set; }
    [SerializeField] private Skills _skills;
    [SerializeField] private Wallet _wallet;
    [Header("Ui")] [SerializeField] private GameObject[] _skillObjects;
    private int[] _skillLevels = new int [5];
    private int[][] _priceSkills;
    private void Awake() => Price = new PriceSkills(); 

    private void Start()
    {
        _skillLevels = new int[]
            { _skills.Ratefire, _skills.BulletSpeed, _skills.EnduranceLevel, _skills.RepairLevel, _skills.DamageLevel };
    }

    private void SetActiveSkill(int skillIndex, int[] priceArray, int skillLevel, GameObject skillObject)
    {
        int maxIndex = priceArray.Length - 1;
        if (skillLevel >= 0 && skillLevel <= maxIndex && priceArray[skillLevel] < _wallet.Money)
        {
            skillObject.SetActive(true);
        }
        else
        {
            skillObject.SetActive(false);
        }
    }
    public void CheckAvailableSkills()
    {
        SetActiveSkill(0, Price.PriceRateFire, _skills.Ratefire, _skillObjects[0]);
        SetActiveSkill(1, Price.PriceBulletSpeed, _skills.BulletSpeed, _skillObjects[1]);
        SetActiveSkill(2, Price.PriceEndurance, _skills.EnduranceLevel, _skillObjects[2]);
        SetActiveSkill(3, Price.PriceRepair, _skills.RepairLevel, _skillObjects[3]);
        SetActiveSkill(4, Price.PriceDamage, _skills.DamageLevel, _skillObjects[4]);

        print(_wallet.Money.ToString());
    }
}