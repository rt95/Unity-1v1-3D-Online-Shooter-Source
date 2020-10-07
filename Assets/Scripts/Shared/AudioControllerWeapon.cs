using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioControllerWeapon : MonoBehaviour
{

    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
            int clipIndex = Random.Range(0, clips.Length);
            AudioClip clip = clips[clipIndex];
            source.PlayOneShot(clip);
    }
}

