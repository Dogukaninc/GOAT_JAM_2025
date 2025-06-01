using _Main.Scripts.Interface;
using _Space_Shooter_Files.Scripts;
using UnityEngine;
using Scripts.GeneralSystems;

public class Bullet : MonoBehaviour
{
    private float damage;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 4f;

    private void Start()
    {
        damage = GeneralValuesHolder.Instance.playerDamage;
    }
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
            other.gameObject.GetComponent<Health>().TakeDamage(damage, other.transform, false, false, iDieable.OnDead);
            Debug.Log("Bullet hit enemy");
            AudioManager.Instance.PlaySound("Hit");
            Destroy(this.gameObject);
        }
    }
}