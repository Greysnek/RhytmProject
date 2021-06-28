using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(LineRenderer))]
public class AudioVisualisation : MonoBehaviour
{
    [SerializeField] private float defaultHeight = 20;
    [SerializeField] private float deltaX = 0.5f;
    [SerializeField] private int spectrumCount = 1024;
    private AudioSource _source;
    private float[] _spectrumData;
    private LineRenderer _line;
    private Vector3[] _dots;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _spectrumData = new float[spectrumCount];

        _line = GetComponent<LineRenderer>();
        _line.positionCount = spectrumCount;

        _dots = new Vector3[spectrumCount];
    }

    private void GetSpectrum()
    {
        _source.GetSpectrumData(_spectrumData, 0, FFTWindow.BlackmanHarris);
    }

    private void ShowSpectrum()
    {
        for (var i = 0; i < spectrumCount; i++)
        {
            _dots[i].x = i * deltaX;
            _dots[i].y = defaultHeight * _spectrumData[i];
        }
        
        _line.SetPositions(_dots);
    }

    private void Update()
    {
        GetSpectrum();
        ShowSpectrum();
    }
}
