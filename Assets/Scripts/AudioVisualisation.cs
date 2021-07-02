using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(PlotRender))]
public abstract class AudioVisualisation : MonoBehaviour
{
    //Relative value of using samples in percents
    [SerializeField] [Range(0, 100)] private float quality = 100;
    //Count of real samples in 1 compressed sample
    private int _absoluteQuality;

    protected AudioSource Source { get; private set; }
    protected AudioClip Clip;
    private PlotRender _plotRender;
    
    private int _frequency;
    private int _sampleCount;

    private float[] _data;
    
    
    private void Awake()
    {
        Source = GetComponent<AudioSource>();
        Clip = Source.clip;
        
        _frequency = Clip.frequency;
        _absoluteQuality = Mathf.RoundToInt(Clip.samples * quality / 100);

        _data = new float[_sampleCount];
        
        _plotRender = GetComponent<PlotRender>();
    }

    protected abstract void GetData(ref float[] data);

    private void Update()
    {
        _plotRender.PaintPosition(Source.time / Clip.length, Color.white);
    }

    private float[] Compress(float[] data)
    {
        _sampleCount = data.Length / _absoluteQuality;
        
        var compressedData = new float[data.Length / _sampleCount];

        for (var i = 0; i < compressedData.Length; i++)
        {
            compressedData[i] = 0;
            for (var j = 0; j < _sampleCount; j++)
            {
                compressedData[i] += Mathf.Abs(data[i * _sampleCount + j]);
            }

            compressedData[i] /= _sampleCount;
        }

        return compressedData;
    }

    public void ShowData()
    {
        GetData(ref _data);
        _plotRender.Create(Compress(_data));
    }
}
