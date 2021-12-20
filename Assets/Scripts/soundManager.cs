using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public static class soundManager
{
    private static string output_mixer_other = "SFX_other";
    private static string output_mixer_car = "SFX_car";
    private static string output_mixer_music = "Music";

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
        BackgroundMusicMenu,
        BackgroundMusicLooting1,
        BackgroundMusicRacing,
        Jump,
        JumpLand,
        TraversePortal,
        BackgroundMusicLootingEgipt,
        BackgroundMusicLootingTutorial,
    }


   private static Dictionary<Sound, float> soundTimerDictionary;
   private static Dictionary<Sound, bool> soundSfxTypeDictionary; //false-car, true-other

    public static void Initialize()
   {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundSfxTypeDictionary = new Dictionary<Sound, bool>();
        soundSfxTypeDictionary[Sound.KartAccelerate] = false;
        soundSfxTypeDictionary[Sound.KartDecelerate] = false;
        soundSfxTypeDictionary[Sound.KartBreaks] = false;
        soundSfxTypeDictionary[Sound.KartHitsObstacle] = true;
        soundSfxTypeDictionary[Sound.KartHitsRamp] = true;
        soundSfxTypeDictionary[Sound.KartNitro] = false;
        soundSfxTypeDictionary[Sound.KartFinishLap] = true;
        soundSfxTypeDictionary[Sound.KartIdle] = false;
        soundSfxTypeDictionary[Sound.PowerUpSound] = true;
        soundSfxTypeDictionary[Sound.KartFlip] = true;
        soundSfxTypeDictionary[Sound.KartStartup] = false;
        soundSfxTypeDictionary[Sound.GrabCarPart] = false;
        soundSfxTypeDictionary[Sound.Jump] = false;
        soundSfxTypeDictionary[Sound.JumpLand] = false;
        soundSfxTypeDictionary[Sound.TraversePortal] = false;
    }

   
   public static void PlaySound(Sound sound)
    { 
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.tag = "Sound";

            AudioMixer mixer = Resources.Load("MasterMixer") as AudioMixer;
            if(soundSfxTypeDictionary.ContainsKey(sound) && !soundSfxTypeDictionary[sound])
            { 
                audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups(output_mixer_car)[0];
            }
            else
            {
            audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups(output_mixer_other)[0];
            }
            audioSource.PlayOneShot(GetAudioClip(sound));
    }
    public static void PlayBackgroundMusic(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Backround");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        AudioMixer mixer = Resources.Load("MasterMixer") as AudioMixer;
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups(output_mixer_music)[0];
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
    }



    //DO WE NEED IT?
    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
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
