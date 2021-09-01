using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager 
{
    public enum Sound 
    {
        Absorption,
        Explosion,
        GravityReversion
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        audioSource.PlayOneShot(GetAudioClip(sound));
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
