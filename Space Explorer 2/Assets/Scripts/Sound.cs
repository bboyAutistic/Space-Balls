﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  {

    public string nameOfSound;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    public AudioMixerGroup outputMixer;

    [HideInInspector]
    public AudioSource source;
}
