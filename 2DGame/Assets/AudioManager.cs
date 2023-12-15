using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    private Sound _sound;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("IdleMusic");
    }

    public static void PlaySoundStatic(string name)
    {
        if (instance == null)
        {
            Debug.LogWarning("No audio manager");
            return;
        }
        instance.Play(name);
    }
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sound.source.Play();
    }
    
    public static void TransitionToStatic(string name)
    {
        if (instance == null)
        {
            Debug.LogWarning("No audio manager");
            return;
        }
        instance.TransitionTo(name);
    }
    
    private void TransitionTo(string sName)
    {
        if (instance == null)
        {
            Debug.LogWarning("No audio manager");
            return;
        }
        StopAllCoroutines();
        if (_sound != null) StartCoroutine(EndSound());
        _sound = Array.Find(sounds, s => s.name == sName);
        if (_sound == null) {
            Debug.LogWarning("Music " + sName + " not found.");
            return;
        }
        StartCoroutine(StartSound());
    }
    
    private IEnumerator EndSound() {
        AudioSource oldSound = _sound.source;
        while (oldSound.volume > 0) {
            oldSound.volume -= 0.01f;
            yield return null;
        }
        oldSound.Stop();
    }
    
    private IEnumerator StartSound() {
        _sound.source.Play();
        float volume = 0f;
        do {
            _sound.source.volume = volume;
            volume += 0.01f;
            yield return null;
        } while (_sound.source.volume <= _sound.volume);
    }

    

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop ();
    }
}
