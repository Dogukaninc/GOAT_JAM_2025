using _Main.Scripts.Interface;
using UnityEngine;

namespace _Main.Scripts.GeneralSystems
{
    public class InteractionReciever : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private SphereCollider _collider;
        
        
        private void Start()
        {
            _collider.radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log("Interactable var");
                
                interactable.Interact();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                Debug.Log("Interactable çıktı");
                
                interactable.UnInteract();
            }
        }

        private void OnValidate()
        {
            _collider.radius = radius;
        }
    }
}