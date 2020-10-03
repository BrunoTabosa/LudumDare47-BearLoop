using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {        
        this.transform.Translate(new Vector3(0, 0.5f, 0));
    }

    public void CancelInteraction()
    {
        this.transform.Translate(new Vector3(0, -0.5f, 0));
    }
}
