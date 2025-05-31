using System.Collections;
using UnityEngine;

public class EnemyCleanse : MonoBehaviour
{
    [Header("Fade Ayarları")] [Tooltip("Partikül sistemi yay-dım (emission) hızının başlangıç değeri")]
    public float initialEmissionRate = 50f;

    [Tooltip("Destroy etmek yerine, sadece partikülleri devre dışı bırakmak istiyorsanız false yapın.")]
    public bool destroyWhenEmpty = true;

    [field: SerializeField] public bool isDamagable { get; private set; } = false;

    [Header("Darkness Ayarları")] [SerializeField, Tooltip("Kalan karanlık miktarı. 0 → tamamen temizlenmiş.")]
    private float darknessAmount = 5f;

    [SerializeField, Tooltip("DarknessAmount'un ilk değeri (sürekli kontrol için)")]
    private float initialDarknessAmount = 5f;

    [Space] [Header("Partikül Sistemi Referansı")] [SerializeField]
    private ParticleSystem darkSmokeParticle;

    private ParticleSystem.EmissionModule emissionModule;
    private bool isFading = false;

    void Awake()
    {
        // Eğer Inspector'dan değeri verecekseniz, initialDarknessAmount = darknessAmount diyebilirsiniz:
        initialDarknessAmount = darknessAmount;
    }

    void Start()
    {
        if (darkSmokeParticle == null)
        {
            Debug.LogError("[EnemyCleanse] darkSmokeParticle referansı atanmamış!");
            return;
        }

        emissionModule = darkSmokeParticle.emission;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(initialEmissionRate);
    }

    public void Cleanse(float speed)
    {
        if (darknessAmount <= 0f)
            return;

        darknessAmount -= Time.deltaTime * speed;
        
        float normalized = Mathf.Clamp01(darknessAmount / initialDarknessAmount);
        float newRate = initialEmissionRate * normalized;
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(newRate);

        if (!isFading)
        {
            isFading = true;
            darkSmokeParticle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }

        if (darknessAmount <= 0f)
        {
            darknessAmount = 0f;
            DestroyWhenEmpty();
            isDamagable = true;
            Debug.Log("<color=green> Enemy is now damagable </color>");
        }
    }

    private void DestroyWhenEmpty()
    {
        Destroy(darkSmokeParticle.gameObject);
    }
}