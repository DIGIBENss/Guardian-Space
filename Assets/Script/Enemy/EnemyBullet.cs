using UnityEngine;

public class EnemyBullet : BaseBullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Station s))
        {
            DoDamage(s.Health);
            Destroy(this.gameObject);
        }
    }

}
