using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadiusStation : MonoBehaviour
{
    public List<EnemyHealth> Enemies = new List<EnemyHealth>();
    [SerializeField] private float _radiusWeapon;
    [SerializeField] private WeaponStation _weaponStation;
    private void OnTriggerEnter(Collider other)
    {
        SearchEnemy();
    }

    private void SearchEnemy()
    {
        var coliders = Physics.OverlapSphere(transform.position, _radiusWeapon);
        foreach (Collider collider in coliders)
        {
            var enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null && !Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
            }
            _weaponStation.TypeWeapon();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusWeapon);
    }
}