using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

	void Awake () {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

		foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.outputMixer;
           
        }
	}

    void Start()
    {
        Play("MainMusic");
    }
	
	
	public void Play (string nameWeSeek)
    {
        Sound s =Array.Find(sounds, sound => sound.nameOfSound == nameWeSeek);
        if (s == null)
        {
            Debug.Log("NO SOUND FOUND!!!");
            return;
        }
        s.source.Play();
	}
}
