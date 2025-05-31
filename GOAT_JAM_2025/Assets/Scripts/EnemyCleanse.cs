using UnityEngine;

public class EnemyCleanse : MonoBehaviour
{
    [field: SerializeField] public bool isDamagable { get; private set; } = false;
    
    [SerializeField] private float darknessAmount;

    private Animator animator;
    private Renderer renderer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    public void Cleanse(float speed)
    {
        darknessAmount -= Time.deltaTime*speed;
        // renderer.material.color = new Color(1, 1, 1, renderer.material.color.a + speed * Time.deltaTime);
        
        if (darknessAmount <= 0)
        {
            isDamagable = true;
            Debug.Log("<color=green> Enemy is now damagable </color>");
        }

        // if (renderer.material.color.a >= 1)
        // {
        //     Debug.Log("Enemy is now damagable");
        //     isDamagable = true;
        //     //animator.SetBool("isDamagable", true);
        // }
    }
}