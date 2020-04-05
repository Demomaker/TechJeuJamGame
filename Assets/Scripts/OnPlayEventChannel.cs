using UnityEngine;

public class OnPlayEventChannel : EventChannel
{
    public event EventHandler Notify;
    public override void Publish()
    {
        Notify?.Invoke();
    }
}