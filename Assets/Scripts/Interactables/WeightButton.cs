using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour
{
    public ButtonState state;

    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private Material unpressedMaterial;
    [SerializeField]
    private Material pressedMaterial;

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
        state = newState;
        print($"Button State: {state}");
        switch(state)
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
