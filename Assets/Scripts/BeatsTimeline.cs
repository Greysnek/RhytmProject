using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlotRender))]
public class BeatsTimeline : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject beatPrefab;

    private Vector3 _arrowPosition;
    private List<GameObject> _beats = new List<GameObject>();

    private float Length => GetComponent<PlotRender>().length;

    private void Start()
    {
        _arrowPosition = arrow.position;
    }

    private void Update()
    {
        _arrowPosition.x = Length * AudioConductor.Main.RelativeSongPosition;
        arrow.position = _arrowPosition;
    }

    public void SetBeats()
    {
        foreach (var beat in _beats)
        {
            Destroy(beat);
        }
        _beats?.Clear();
        
        foreach (var relativeBeatTime in AudioConductor.Main.RelativeBeatsTimes)
        {
            var newBeat = Instantiate(beatPrefab, transform, true);
            var beatPosition = newBeat.transform.position;
            beatPosition.x = Length * relativeBeatTime;
            newBeat.transform.position = beatPosition;
            
            _beats.Add(newBeat);
        }
    }
}
