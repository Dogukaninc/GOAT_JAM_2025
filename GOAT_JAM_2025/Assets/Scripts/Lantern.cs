using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float cleanseSpeed = 0.1f;
    [SerializeField] private Animator lanternAnimator;
    [SerializeField] private float lanternLightCharge;
    [SerializeField] private float chargeValueMultiplier = 1f;
    [SerializeField] private Slider lanternChargeSlider;

    public bool isLanternOn;
    private Collider collider;
    private Light light;
    private float maxLanternLightCharge = 100f;
    private Tween _sliderTween;

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
            UpdateLanternUI();
        }
        else
        {
            if (lanternLightCharge >= maxLanternLightCharge) return;
            lanternLightCharge += Time.deltaTime * chargeValueMultiplier;
            UpdateLanternUI();
        }
    }
    
    public void UpdateLanternUI()
    {
        var roundedHealth = (float)System.Math.Round(lanternLightCharge, 2);

        if (lanternChargeSlider != null)
        {
            if (_sliderTween != null && _sliderTween.IsActive()) _sliderTween.Kill();
            _sliderTween = DOVirtual.Float(lanternChargeSlider.value, roundedHealth / maxLanternLightCharge, 0.3f, x => { lanternChargeSlider.value = x; });
        }
    }

    private void UpdateSlider()
    {
        lanternChargeSlider.value = lanternLightCharge / maxLanternLightCharge;
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