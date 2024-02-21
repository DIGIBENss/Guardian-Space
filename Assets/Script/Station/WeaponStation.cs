using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStation : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _rateFire = 1f;
    [SerializeField] private RadiusStation _radiusStation;
    [SerializeField] private GameObject _prefabBullet;
    [SerializeField] private Transform _shoot;

    private Transform _cached;
    private bool _isDamage;
    public float _shootInterval = 0.5f; // Время между выстрелами
    private float _lastShootTime;
    private void Update()
    {
        if (Input.GetKey(KeyCode.S) && Time.time - _lastShootTime > _shootInterval)
        {
            var bullet = Instantiate(_prefabBullet, _shoot.position, Quaternion.identity);
            _lastShootTime = Time.time;
        }
    }

    private void Awake()
    {
        _cached = transform;
    }

    public void TypeWeapon()
    {
        foreach (var target in _radiusStation.Enemies)
        {
            if (target != null && _isDamage)
            {
                Vector3 playerPosition = target.transform.position;
                Vector3 directionToPlayer = (playerPosition - _shoot.position).normalized; 
                Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer); 
                Instantiate(_prefabBullet, _shoot.position, bulletRotation); 
                //target.TakeDamage(_damage, _rateFire);
                _isDamage = false;
                break;
            }
        }
    }

    private void Start()
    {
        _cached = transform;
        InvokeRepeating("Recharge", 0, _rateFire);
    }

    private void Recharge()
    {
        _isDamage = true;
    }
}