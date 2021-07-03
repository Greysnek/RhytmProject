public class AudioWaveVisualisation : AudioVisualisation
{
    protected override void GetData(ref float[] data)
    {
        data = new float[Clip.samples * Clip.channels];
        Clip.GetData(data, 0);
    }

    private void Start()
    {
        ShowData();
    }
}
