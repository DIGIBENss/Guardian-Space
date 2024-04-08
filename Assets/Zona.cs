using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zona : MonoBehaviour
{
    public List<EnemyHealth> Enemys;
    public BoxCollider Box;
    private float _newWidth =10, _newDepth= 10;
    private float _additional = 2;
    
    public void UP() =>  Box.size += new Vector3(_additional, 0, _additional);
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemy))
        {
            Enemys.Add(enemy);
            enemy.OnDiee += (x) => Enemys.Remove(x);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth enemy))
        {
            Enemys.Remove(enemy);
            enemy.OnDiee -= (x) => Enemys.Remove(x);
        }
    }
}