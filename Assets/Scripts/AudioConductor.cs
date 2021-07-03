using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioConductor : MonoBehaviour
{
    [NonSerialized] public static AudioConductor Main;
    
    [SerializeField] private float beatsPerMinute;
    [SerializeField] private float firstBeatOffset;
    [SerializeField] private bool playOnAwake;
    [SerializeField] private UnityEvent onPlay;
    [SerializeField] private UnityEvent onBeat;
    
    private AudioSource _source;
    private IEnumerator _playing;
    private float _startSongTime;

    private float SongPositionInSeconds => _source.time;
    public float SongPositionInBeats => (SongPositionInSeconds - firstBeatOffset) / SecPerBeat;
    public float RelativeSongPosition => SongPositionInSeconds / _source.clip.length;
    private float SecPerBeat => 60f / beatsPerMinute;
    public float[] BeatsTimes
    {
        get
        {
            var beatsTimes = new List<float>();

            for (var currentBeatTime = firstBeatOffset; currentBeatTime < _source.clip.length; currentBeatTime += SecPerBeat)
            {
                beatsTimes.Add(currentBeatTime);
            }

            return beatsTimes.ToArray();
        }
    }
    public float[] RelativeBeatsTimes
    {
        get
        {
            var relativeBeatsTimes = new List<float>();
            
            foreach (var beatTime in BeatsTimes)
            {
                relativeBeatsTimes.Add(beatTime / _source.clip.length);
            }

            return relativeBeatsTimes.ToArray();
        }
    }


    private void Awake()
    {
        Main = this;

        _playing = Playing();
        
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;

        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        StopCoroutine(_playing);
        StartCoroutine(_playing);
    }

    private IEnumerator Playing()
    {
        _source.Play();
        onPlay?.Invoke();
        
        float currentAfterBeatTime = 0;
        
        while (SongPositionInSeconds < _source.clip.length)
        {
            currentAfterBeatTime += Time.deltaTime;
            
            if (currentAfterBeatTime >= SecPerBeat)
            {
                onBeat?.Invoke();
                currentAfterBeatTime = 0;
            }
            
            yield return null;
        }
    }
}
