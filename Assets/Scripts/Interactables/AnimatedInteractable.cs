using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteractable : MonoBehaviour, IInteractable
{
    public Animation animation;
    public AnimationClip InteractClip;
    public AnimationClip CancelInteractClip;

    public int ActionsRequired = 1;
    private int currentActions = 0;

    void Start()
    {
        currentActions = 0;
    }

    public void CancelInteraction()
    {
        currentActions--;
        if (CancelInteractClip)
        {
            animation.clip = CancelInteractClip;
            animation.Play();
        }
    }

    public void Interact()
    {
        currentActions++;
        if (currentActions < ActionsRequired) return;
        if (InteractClip)
        {
            animation.clip = InteractClip;
            animation.Play();
        }
    }
}
