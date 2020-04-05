using UnityEngine;

public class OnMusicVolumeChanged : EventChannel<float>
{
    public event EventHandler<float> Notify;
    public override void Publish(float parameter)
    {
        Notify?.Invoke(parameter);
    }
}