using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager2 : Singleton<SoundManager2>
{
    public AudioMixer audioMixer;
    public AudioSource audioBackGround;
    bool flagEffect = true;
    public List<AudioSource> hiteffect;
    public void Awake()
    {
        Init(80, 80);
    }

    public void Init(float musicVolume, float sfxvolume)
    {
        audioMixer.SetFloat(Constan.BACKGROUND_VOLUME, musicVolume);
        audioMixer.SetFloat(Constan.EFFECT_VOLUME, sfxvolume);
        foreach (AudioSource x in hiteffect)
        {
            Debug.Log(x.clip.name);
        }
    }

    public void PlaySound(string name)
    {
        if (!flagEffect)
        {
            return;
        }
        for (int i = 0; i < hiteffect.Count; i++)
            if (hiteffect[i].clip.name == name)
                hiteffect[i].Play();
    }
    public void StopSound(string name)
    {

        for (int i = 0; i < hiteffect.Count; i++)
            if (hiteffect[i].clip.name == name)
                hiteffect[i].Stop();
    }
    public void SwitchSoundBackGround()
    {

        if (audioBackGround.isPlaying)
            audioBackGround.Stop();
        else
            audioBackGround.Play();
    }
    public void ChangeVolumeBackground(float volume)
    {
        audioMixer.SetFloat("BackgroundVolume", volume);
    }

    public void ChangeVolumeEffect(float volume)
    {
        audioMixer.SetFloat("EffectVolume", volume);
    }
    public void SwitchMusicEffect()
    {
        flagEffect = !flagEffect;
    }
    public void UpdateVolume(Slider slider)
    {
        audioBackGround.volume = slider.value;
    }
}
