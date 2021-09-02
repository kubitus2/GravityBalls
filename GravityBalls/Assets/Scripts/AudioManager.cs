using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager 
{
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public enum Sound 
    {
        Absorption,
        Explosion,
        GravityReversion
    }

    public static void PlaySound(Sound sound)
    {
        if(oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One shot sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        AudioClip clip = null;
        foreach (SoundAssets.SoundAudioClip audioClip in SoundAssets.i.soundAudioClipArray)
        {
            Debug.Log(audioClip.sound);
            if(audioClip.sound == sound)
            {
                clip = audioClip.clip;
            }
        }

        return clip;
    }

}
