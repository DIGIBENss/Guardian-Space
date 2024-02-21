using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeDuration = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<HealthStation>() != null)
        {
            HealthStation.Singleton.TakeDamage(10,0);
            Destroy(gameObject);
        }
    }
}
