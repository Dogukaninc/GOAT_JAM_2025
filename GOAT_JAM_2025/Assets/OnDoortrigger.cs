using UnityEngine;

public class OnDoortrigger : MonoBehaviour
{
    public GameEvent OnDoortriggerEvent;
    public LevelManager levelManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDoortriggerEvent.Raise(this, null);
            gameObject.SetActive(false);
        }
    }
}