using UnityEngine;

public class PlayRandomizedClipOnTriggerEnterAction : AbstractOnTriggerEnterAction
{
    public float volumeScale = 1.0f;

    public AudioSource usedAudioSource;

    public RandomAudioClipsController randomAudioClipsController;

    protected override void DoAction(Collider other)
    {
        randomAudioClipsController.PlayOneShot(usedAudioSource, volumeScale);
    }
}
