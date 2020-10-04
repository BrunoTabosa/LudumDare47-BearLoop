using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteractable : MonoBehaviour, IInteractable
{
    public Animation animation;
    public AnimationClip InteractClip;
    public AnimationClip CancelInteractClip;

    void Start()
    {
        //animation.AddClip(InteractClip, InteractClip.name);
        //animation.AddClip(CancelInteractClip, CancelInteractClip.name);
    }

    public void CancelInteraction()
    {
        if(CancelInteractClip)
        {
            animation.clip = CancelInteractClip;
            animation.Play();
        }
    }

    public void Interact()
    {
        if (InteractClip)
        {
            animation.clip = InteractClip;
            animation.Play();
        }
    }
}
