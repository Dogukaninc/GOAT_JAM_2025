using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float cleanseSpeed = 0.1f;    
    private Collider collider;
    private Light light;

    void Awake()
    {
        light = transform.parent.GetComponentInChildren<Light>();
        collider = GetComponent<Collider>();
    }

    public void OnLanternOn()
    {
        collider.enabled = !collider.enabled;
        light.enabled = !light.enabled;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCleanse>().Cleanse(cleanseSpeed);
        }
    }

}
