using _Main.Scripts.Interface;
using UnityEngine;
using Scripts.GeneralSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 4f;

    public void Thrown()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyCleanse>().isDamagable)
        {
            var iDieable = other.gameObject.GetComponent<IDieable>();
            other.gameObject.GetComponent<Health>().TakeDamage(damage, null, false, false, iDieable.OnDead);
            Debug.Log("Bullet hit enemy");
            Destroy(this.gameObject);
        }
    }
}