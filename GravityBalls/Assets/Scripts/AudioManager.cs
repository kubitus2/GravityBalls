using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager
{
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static float volMultiplier;

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

        oneShotAudioSource.outputAudioMixerGroup = GetMixerGroup(sound);
        if(sound == Sound.Explosion)
            volMultiplier =0.3f;
        else
            volMultiplier = 1.0f;        

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), volMultiplier);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        AudioClip clip = null;
        foreach (SoundAssets.SoundAudioClip audioClip in SoundAssets.Instance.soundAudioClipArray)
        {
            if(audioClip.sound == sound)
            {
                clip = audioClip.clip;
            }
        }

        return clip;
    }

    private static AudioMixerGroup GetMixerGroup(Sound sound)
    {
        AudioMixerGroup audioMixerGroup = null;

        foreach (SoundAssets.SoundAudioClip audioClip in SoundAssets.Instance.soundAudioClipArray)
        {
            if(audioClip.sound == sound)
            {
                audioMixerGroup = audioClip.targetAMGroup;
            }
        }

        return audioMixerGroup;
    }

    private static IEnumerator Wait()
    {
        yield return null;
    }

}
