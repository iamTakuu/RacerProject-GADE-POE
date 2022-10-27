using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BMGManager : MonoBehaviour
{
    [SerializeField] private BMGScriptableObject _musicSo;
    [SerializeField] private AudioSource _source1;
    [SerializeField] private AudioSource _source2;
    [SerializeField] private float _bmgVolume = 1f;
    [SerializeField] [Range(10, 40)] private float fadeTime;

    private bool firstTrack = true;

    //private AudioSource currentSource;

    //private int _songIndex = 0;

    private void Awake()
    {
        _source1.clip = _musicSo.GetSong(0);
        _source1.volume = 0f;
        _source1.Play();
        FadeIn(_source1);
        firstTrack = false;

    }
    private void PlayNextSong()
    {
        StopCoroutine(TrackCurrentSong());
        if (_source1.isPlaying)
        {
            StartCoroutine(CrossFade(_source2, _source1));
        }
        else
        {
            StartCoroutine(CrossFade(_source1, _source2));
        }
    }
    private void FadeIn(AudioSource source)
    {
        if (!firstTrack)
        {
            source.clip = _musicSo.GetSong(1);
        }
        source.volume = 0f;
        source.Play();
        StartCoroutine(TrackCurrentSong(source));
        source.DOFade(_bmgVolume, fadeTime);
    }
    private float FadeOut(AudioSource source)
    {
        source.DOFade(0, fadeTime/2);
        return fadeTime/2;
    }
    private IEnumerator CrossFade(AudioSource inSource, AudioSource outSource)
    {
        FadeIn(inSource);
        yield return new WaitForSeconds(FadeOut(outSource));
        outSource.Stop();
    }
    
    //This bugs out if you don't wait at least
    //20% of the song.
    private IEnumerator TrackCurrentSong(AudioSource currentSource = null)
    { 
        yield return new WaitForSeconds(currentSource.clip.length * 0.9f);
        PlayNextSong();
    }
    
    }
