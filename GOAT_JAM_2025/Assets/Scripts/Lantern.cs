using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private float cleanseSpeed = 1f;
    private Light light;


    void Awake()
    {
        light = GetComponent<Light>();
        collider = GetComponent<Collider>();
    }


    public void OnLanternOn()
    {
        Debug.Log("Lantern on");
        collider.enabled = !collider.enabled;
        light.enabled = !light.enabled;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, other.gameObject.GetComponent<Renderer>().material.color.a + Time.deltaTime * cleanseSpeed);
        }
    }

}
