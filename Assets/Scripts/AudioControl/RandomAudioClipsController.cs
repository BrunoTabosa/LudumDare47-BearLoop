using UnityEngine;

public class RandomAudioClipsController : MonoBehaviour
{
    public AudioSource usedAudioSource;

    public AudioClip[] randomizedAudioClips;

    private AudioClip randomizedAudioClip;

    public void PlayOneShot( AudioSource usedAudioSource = null, float volumeScale = 1.0f  )
    {
        if( usedAudioSource == null )
        {
            usedAudioSource = this.usedAudioSource;
        }

        if( randomizedAudioClips != null && randomizedAudioClips.Length > 0 )
        {
            randomizedAudioClip = randomizedAudioClips[Random.Range(0, randomizedAudioClips.Length)];

            //Debug.Log( "Playing Random Clip named: " + randomizedAudioClip.name );

            usedAudioSource.PlayOneShot(randomizedAudioClip, volumeScale);
        }
    }
}
