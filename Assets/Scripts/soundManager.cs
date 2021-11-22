using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class soundManager
{
   public enum Sound
    {
        KartAccelerate,
        KartDecelerate,
        KartBreaks,
        KartHitsObstacle,
        KartHitsRamp,
        KartNitro,
        KartFinishLap,
        KartIdle,
    }

   private static Dictionary<Sound, float> soundTimerDictionary;

   public static void Initialize()
   {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.KartIdle] = 0.0f;
   }

   
   public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.KartIdle:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float kartAccelerateTimerMax = .05f;
                    if(lastTimePlayed + kartAccelerateTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                break;
            default:
                return true;
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }
}
