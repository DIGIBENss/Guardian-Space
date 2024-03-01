using UnityEngine;

public class BulletStation : BaseBullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyHealth e))
        {
            DoDamage(e);
            Destroy(this.gameObject);
        }
    }


}
