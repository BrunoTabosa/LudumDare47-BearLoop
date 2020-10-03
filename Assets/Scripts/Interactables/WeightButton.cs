using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour, IInteractor
{
    private ButtonState state;
    [SerializeField]
    private List<GameObject> InteractablesObjects;

    public List<IInteractable> Interactables { get; set; }

    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private Material unpressedMaterial;
    [SerializeField]
    private Material pressedMaterial;

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
        foreach (var interactable in Interactables)
        {
            interactable.Interact();
        }
    }

    public void CancelInteraction()
    {
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

    private void OnTriggerStay(Collider other)
    {
        SetState(ButtonState.Pressed);
    }

    private void SetState(ButtonState newState)
    {
        if (state == ButtonState.Unpressed && newState == ButtonState.Pressed)
        {
            Interact();
        }
        else if(state == ButtonState.Pressed && newState == ButtonState.Unpressed)
        {
            CancelInteraction();
        }

        state = newState;

        switch (state)
        {
            case ButtonState.Unpressed:
                renderer.material = unpressedMaterial;
                break;
            case ButtonState.Pressed:
                renderer.material = pressedMaterial;
                break;
        }
    }
}
