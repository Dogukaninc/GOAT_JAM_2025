using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float cleanseSpeed = 0.1f;
    [SerializeField] private Animator lanternAnimator;
    [SerializeField] private float lanternLightCharge;
    [SerializeField] private float chargeValueMultiplier = 1f;

    public bool isLanternOn;
    private Collider collider;
    private Light light;

    void Awake()
    {
        light = transform.parent.GetComponentInChildren<Light>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        OnLanternOff();
    }

    private void Update()
    {
        if (isLanternOn)
        {
            if (lanternLightCharge <= 0) return;
            lanternLightCharge -= Time.deltaTime * chargeValueMultiplier;
        }
        else
        {
            lanternLightCharge += Time.deltaTime * chargeValueMultiplier;
        }
    }

    public void OnLanternOn()
    {
        if (lanternLightCharge <= 0) return;

        collider.enabled = !collider.enabled;
        light.enabled = true;
        lanternAnimator.SetBool("isLightOn", true);
        isLanternOn = true;
    }

    public void OnLanternOff()
    {
        light.enabled = false;
        lanternAnimator.SetBool("isLightOn", false);
        isLanternOn = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCleanse>().Cleanse(cleanseSpeed);
        }
    }
}