using UnityEngine;

public class OnSoundPlayEventChannel : EventChannel<GameObject, AudioClip>
{
    public event EventHandler<GameObject, AudioClip> Notify;
    public override void Publish(GameObject source, AudioClip sound)
    {
        Notify?.Invoke(source, sound);
    }
}