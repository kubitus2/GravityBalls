using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _i;

    public static SoundAssets i 
    {
        get
        {
            if (_i == null)
                _i = (Instantiate(Resources.Load("main")) as GameObject).GetComponent<SoundAssets>();
            return _i; 
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip 
    {
        public AudioManager.Sound sound;
        public AudioClip clip;
    }
}
