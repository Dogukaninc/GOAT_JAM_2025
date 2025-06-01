using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public void DestroySelfParticle()
    {
        Destroy(this.gameObject, 1f);
    }
}
