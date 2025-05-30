using UnityEngine;

using Scripts.GeneralSystems;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 4f;

    public void Thrown(){
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
        Destroy(gameObject,lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyCleanse>().isDamagable)
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("Bullet hit enemy");
            Destroy(gameObject);
        }
    }

}
