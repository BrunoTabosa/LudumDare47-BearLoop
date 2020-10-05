
using UnityEngine;
using UnityEngine.Events;

public class InteractibleLight : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UnityEvent _onInteract;
    [SerializeField]
    private UnityEvent _onCancelInteract;

    public void CancelInteraction()
    {
        _onCancelInteract?.Invoke();
    }

    public void Interact()
    {
        _onInteract?.Invoke();
    }
}
