using UnityEngine;

public class OnWinEventChannel : EventChannel
{
    public event EventHandler Notify;
    public override void Publish()
    {
        Notify?.Invoke();
    }
}