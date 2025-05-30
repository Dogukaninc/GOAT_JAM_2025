using UnityEngine;

public class EnemyCleanse : MonoBehaviour
{
    public bool isDamagable {get; private set; } = false;
    private Animator animator;
    private Renderer renderer;
    void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
    }

    public void Cleanse(float speed)
    {
        renderer.material.color = new Color(1, 1, 1, renderer.material.color.a + speed * Time.deltaTime);
        Debug.Log(renderer.material.color.a);

        if (renderer.material.color.a >= 1)
        {
            Debug.Log("Enemy is now damagable");
            isDamagable = true;
            //animator.SetBool("isDamagable", true);
        }
    }

    
}
