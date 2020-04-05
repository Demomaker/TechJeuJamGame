using UnityEngine;

public class OnSoundVolumeChanged : EventChannel<float>
{
    public event EventHandler<float> Notify;
    public override void Publish(float parameter)
    {
        Notify?.Invoke(parameter);
    }
}