public class OnLossEventChannel : EventChannel
{
    public event EventHandler Notify;
    public override void Publish()
    {
        Notify?.Invoke();
    }
}