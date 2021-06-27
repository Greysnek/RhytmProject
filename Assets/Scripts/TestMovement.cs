using UnityEngine;

public class TestMovement : MonoBehaviour
{
    private Vector3 Scale => _isOddBeat ? Vector3.one : Vector3.one * -1;

    private bool _isOddBeat;

    [SerializeField] private float speed = 2;

    private float _oldBeatTime;

    private bool _switchingIsAllow;
    public void AllowSwitch()
    {
        if (AudioTimeConductor.Main.NearestBeatDistance < 0)
        {
            SwitchScale();
            return;
        }
        
        _switchingIsAllow = true;
    }
    public void SwitchScale()
    {
        _isOddBeat = !_isOddBeat;
        _switchingIsAllow = false;
    }
    public void Update()
    {
        transform.localScale += Scale * (speed * AudioTimeConductor.Main.SongDeltaTime) / AudioTimeConductor.Main.SecPerBeat;
    }
}
