using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour, IInteractor
{
    public Animation animation;
    public AnimationClip Pressed;
    public AnimationClip Unpressed;
    private ButtonState state = ButtonState.Unpressed;

    [SerializeField]
    private List<GameObject> InteractablesObjects;

    public List<IInteractable> Interactables { get; set; }

    public void Start()
    {
        Interactables = new List<IInteractable>();
        foreach (var item in InteractablesObjects)
        {
            Interactables.Add(item.GetComponent<IInteractable>());
        }
    }

    public void Interact()
    {
        if (Pressed != null)
        {
            animation.clip = Pressed;
            animation.Play();
        }
        foreach (var interactable in Interactables)
        {
            interactable.Interact();
        }
    }

    public void CancelInteraction()
    {
        if (Unpressed != null)
        {
            animation.clip = Unpressed;
            animation.Play();
        }

        foreach (var interactable in Interactables)
        {
            interactable.CancelInteraction();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //SetState(ButtonState.Pressed);

    }
    void OnTriggerExit(Collider collider)
    {
        SetState(ButtonState.Unpressed);
    }

    void OnTriggerStay(Collider other)
    {
        SetState(ButtonState.Pressed);
    }

    private void SetState(ButtonState newState)
    {
        if (state == ButtonState.Unpressed && newState == ButtonState.Pressed)
        {
            Interact();
        }
        else if (state == ButtonState.Pressed && newState == ButtonState.Unpressed)
        {
            CancelInteraction();
        }

        state = newState;
    }
}
