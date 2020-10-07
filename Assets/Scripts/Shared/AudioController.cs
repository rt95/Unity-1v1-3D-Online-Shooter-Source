using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    public void Play()
    {
        if (!canPlay)
        {
            return;
        }

        //GameManager.Instance.Timer.add(() => {
        //    canPlay = true;
        //}, delayBetweenClips);

        delayBetweenClips -= Time.deltaTime;
        if (delayBetweenClips < 0)
        {
            delayBetweenClips = 0.2f;
            int clipIndex = Random.Range(0, clips.Length);
            AudioClip clip = clips[clipIndex];
            source.PlayOneShot(clip);
        }
    }
}
