using System;
using UnityEngine;
using UnityEngine.Events;

public class InputSync : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private int accuracyPercent = 40;
    [SerializeField] private UnityEvent OnClick;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Mathf.Abs(AudioTimeConductor.Main.NearestBeatDistance) < AudioTimeConductor.Main.SecPerBeat * accuracyPercent / 200 + Time.deltaTime)
            {
                OnClick?.Invoke();
                Debug.LogWarning("Ok");
            }
            else
            {
                Debug.LogError("Ok");
            }
        }
    }
}
