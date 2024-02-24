using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Action _shot;
    [SerializeField] SearchPlayer _searchPlayer;
    [SerializeField] Transform _positionShooting;
    [SerializeField] GameObject _bulletShooting;
    public float fireRate = 0.5f; 
    private float nextFireTime = 0f;
    private void OnEnable()
    {
        _shot += Shooting;
    }
    private void OnDisable()
    {
        _shot -= Shooting;
    }
    private void Start()
    {
        
    }
    private void Shooting()
    {
        if (Time.time >= nextFireTime)
        {
            Vector3 playerPosition = _searchPlayer.GetPlayerPosition();
            Debug.Log("Player Position: " + playerPosition); // Выводим позицию игрока в консоль
            Vector3 directionToPlayer = (playerPosition - _positionShooting.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(directionToPlayer);
            Debug.Log("_positionShooting: " + _positionShooting.position);
            Debug.Log("Bullet Rotation: " + bulletRotation); // Выводим поворот пули в консоль
            Instantiate(_bulletShooting, _positionShooting.position, bulletRotation); 
            nextFireTime = Time.time + fireRate; 
        }
    }
}
