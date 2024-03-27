using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public float MaxHealth { get; }
    public float Health { get; }
    public void TakeDamage(float damage);
    public void TakeHeal(float value);
}
