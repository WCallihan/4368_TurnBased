using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper {
    
    public static AudioSource PlayClip2D(AudioClip clip, float volume = 1) {
        //create
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure
        audioSource.clip = clip;
        audioSource.volume = volume;
        //activate
        audioSource.Play();
		Object.Destroy(audioSource, clip.length);
        //return
        return audioSource;
    }
}