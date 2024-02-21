using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStation : MonoBehaviour
{
    public float speed = 10f;
    public float lifeDuration = 1f;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnColliderEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            print("enemy");
            var enemy = other.gameObject.GetComponent<EnemyHealth>();
            enemy.TakeDamage(50,0);
           
        }
    }
}
