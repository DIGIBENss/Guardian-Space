using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceSkills
{
    public int[] PriceInfluence = new int[] {100,300,500,710,1200,1500,250};
    public int[] PriceManeuverability = new int[] {100,320,500,700,1230,1500,250};
    public int[] PriceEndurance = new int[] {100,300,530,700,1200,1500,250};
    public int[] PriceRepair = new int[] {120,300,500,700,1200,1500,250};
    public int[] PriceDamage = new int[] {100,310,500,750,1200,1510,250};
    
    public int GetPrice(int skillIndex, int level)
    {
        switch (skillIndex)
        {
            case 0: return PriceInfluence[level];
            case 1: return PriceManeuverability[level];
            case 2: return PriceEndurance[level];
            case 3: return PriceRepair[level];
            case 4: return PriceDamage[level];
            default: return 0;
        }
    }
}

