using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _ambientSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio clips")]
    [SerializeField] private AudioClip[] _randomAmbiantClips;
    
    [Header("Random sound generation")]
    [SerializeField] private float _minDelayBetweenAmbiantSounds = 5f;
    [SerializeField] private float _maxDelayBetweenAmbiantSounds = 10f;
    
    private void Start()
    {
        StartCoroutine(PlayAmbiantAfter());
    }

    public IEnumerator PlayAmbiantAfter()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minDelayBetweenAmbiantSounds, _maxDelayBetweenAmbiantSounds));
        var randomAmbiantClip = _randomAmbiantClips[UnityEngine.Random.Range(0, _randomAmbiantClips.Length)];
        _ambientSource.PlayOneShot(randomAmbiantClip);

        yield return new WaitUntil(() => !_ambientSource.isPlaying);
        StartCoroutine(PlayAmbiantAfter());
    }
}
