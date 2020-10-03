using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
    List<IInteractable> Interactables { get; set; }
    void Interact();
}
