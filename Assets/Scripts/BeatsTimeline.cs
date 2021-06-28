using UnityEngine;

public class BeatsTimeline : MonoBehaviour
{
    [SerializeField] private float startX;
    [SerializeField] private float finishX;
    [SerializeField] private Transform arrow;
    [SerializeField] private GameObject beat;

    private Vector3 _arrowPosition;

    private float Length
    {
        get
        {
            var length = finishX - startX;
            if (!(length < 0)) return length;
            
            Debug.LogError("Wrong start & finish coordinates");
            return 0;
        }
    }

    private void Start()
    {
        _arrowPosition = arrow.position;
    }

    private void Update()
    {
        _arrowPosition.x = startX + Length * AudioTimeConductor.Main.RelativeSongPosition;
        arrow.position = _arrowPosition;
    }

    public void SetBeats()
    {
        foreach (var relativeBeatTime in AudioTimeConductor.Main.RelativeBeatTimes)
        {
            var newBeat = Instantiate(beat, transform, true);
            var beatPosition = newBeat.transform.position;
            beatPosition.x = startX + Length * relativeBeatTime;
            newBeat.transform.position = beatPosition;
        }
    }
}
