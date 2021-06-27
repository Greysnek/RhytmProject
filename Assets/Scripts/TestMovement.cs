using System;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    private Vector3 Scale => _isOddBeat ? Vector3.one : Vector3.one * -1;

    private bool _isOddBeat;

    [SerializeField] private float speed = 2;

    private float _oldBeatTime;

    public void SwitchScale()
    {
        _isOddBeat = !_isOddBeat;

        var error = Mathf.Abs(Time.time - _oldBeatTime - AudioTimeConductor.Main.SecPerBeat);
        _oldBeatTime = Time.time;
        
        if (error > 0.0085)
        {
            Debug.LogWarning("accuracy: +-" + error);
        }
        else
        {
            Debug.Log("accuracy: +-" + error);
        }   
    }
    public void Update()
    {
        transform.localScale += Scale * speed * AudioTimeConductor.Main.SongDeltaTime / AudioTimeConductor.Main.SecPerBeat;
    }
}
