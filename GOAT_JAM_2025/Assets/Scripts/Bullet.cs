using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 4f;
    private GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyCleanse>().isDamagable)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("Bullet hit enemy");
            DespawnBullet();
        }
    }

    public void SetupBullet(GameObject player){
        transform.position = player.GetChildWithTag("Magic").transform.position;
        transform.rotation = player.GetChildWithTag("Magic").transform.rotation;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Invoke(nameof(DespawnBullet), lifeTime);
    }

    private void DespawnBullet()
    {
        player.GetComponent<Player>().bulletPool.ReturnBullet(gameObject);
    }
}
