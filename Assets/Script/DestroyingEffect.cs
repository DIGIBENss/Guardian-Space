using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingEffect : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("CheckHpStation", 0,1);
    }

    private void CheckHpStation()
    {
        if (Station.Singleton.Health.Health > Station.Singleton.Health.MaxHealth / 2)
        {
            Station.Singleton.IsCreateStation = true;
            Destroy(gameObject);
        }
    }
}
