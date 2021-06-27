using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioTimeConductor : MonoBehaviour
{
    [NonSerialized] public static AudioTimeConductor Main;
    
    [SerializeField] private float beatsPerMinute;
    [SerializeField] private float firstBeatOffset;
    [SerializeField] private bool playOnAwake;
    [SerializeField] private UnityEvent beat;
    
    public float SongPositionInSeconds
    {
        get
        {
            if (_isPlaying)
            {
                return (float) (AudioSettings.dspTime - _dspSongTime - firstBeatOffset);
            }

            return 0;
        }
    }
    public float SongPositionInBeats
    {
        get
        {
            if (SecPerBeat != 0)
            {
                return SongPositionInSeconds / SecPerBeat;
            }
            
            Debug.LogError("Your beat too fast :)");
            
            return 0;
        }
    }
    public float SongDeltaTime => SongPositionInSeconds - _oldSongPosition;
    public float GlobalSongPosition => SongPositionInSeconds + _startSongTime + firstBeatOffset;
    public int NearestBeat => Mathf.RoundToInt(SongPositionInBeats);
    public float NearestBeatDistance => NearestBeat * SecPerBeat - SongPositionInSeconds;
    public float SecPerBeat { get; private set; }

    
    private AudioSource _musicSource;
    private bool _isPlaying;
    private IEnumerator _playing;
    private float _dspSongTime;
    private float _oldSongPosition;
    private float _currentAfterBeatTime;
    private float _startSongTime;
    

    private void Awake()
    {
        Main = this;
        
        _musicSource = GetComponent<AudioSource>();
        _musicSource.playOnAwake = false;

        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        SecPerBeat = 60f / beatsPerMinute;
        _dspSongTime = (float) AudioSettings.dspTime;
        
        _isPlaying = true;
        _musicSource.Play(0);

        _currentAfterBeatTime = 0;

        _startSongTime = Time.time;
    }

    private void Update()
    {
        if (_isPlaying && SongPositionInSeconds < _musicSource.clip.length)
        {
            _currentAfterBeatTime += Time.deltaTime;
            if (!(_currentAfterBeatTime > SecPerBeat)) return;
            _currentAfterBeatTime = 0;
            
            beat?.Invoke();

            return;
        }

        _isPlaying = false;
    }
    
    private void LateUpdate()
    {
        _oldSongPosition = SongPositionInSeconds;
    }
}
