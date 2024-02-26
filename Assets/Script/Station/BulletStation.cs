using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStation : MonoBehaviour
{
    public float speed = 2f;
    public float lifeDuration = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeDuration);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            print("enemy");
            var enemy = other.gameObject.GetComponent<EnemyHealth>();
            enemy.TakeDamage(20, 0);
            Destroy(gameObject);
        }
    }


    //public float speed = 10f;
    //public float lifeDuration = 1f;

    //private Rigidbody rb;

    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.velocity = transform.forward * speed;
    //    Destroy(gameObject, lifeDuration);
    //}
    ////private void Update()
    ////{
    ////    Vector2 movement = new Vector2(transform.forward.x, transform.forward.z);
    ////    transform.Translate(movement * speed * Time.deltaTime);
    ////    //transform.Translate(speed * Time.deltaTime * Vector3.forward);
    ////}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<EnemyHealth>() != null)
    //    {
    //        print("enemy");
    //        var enemy = other.gameObject.GetComponent<EnemyHealth>();
    //        enemy.TakeDamage(50, 0);
    //        Destroy(gameObject);
    //    }
    //}
}
