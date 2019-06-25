namespace ZigZag.Controller
{
    public interface IPooler
    {
        void AddToPool( IPoolable item );
        IPoolable GetItem();
        void Clear();
    }
}