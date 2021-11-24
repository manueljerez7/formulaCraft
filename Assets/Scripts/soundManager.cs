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
        PowerUpSound,
        KartFlip,
        KartStartup,
        GrabCarPart,
    }

   private static Dictionary<Sound, float> soundTimerDictionary;

   public static void Initialize()
   {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.KartAccelerate] = 0.1f;
        AudioListener.volume = GameAssets.i.masterVolume;
   }

   
   public static void PlaySound(Sound sound)
    { 
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

   
            audioSource.PlayOneShot(GetAudioClip(sound));
            // audioSource.Stop();

    }

    public static void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.KartIdle:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float kartAccelerateTimerMax = .001f;
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
