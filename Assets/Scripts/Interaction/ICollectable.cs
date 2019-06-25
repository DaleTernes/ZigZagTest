using UnityEngine;
using ZigZag.Controller;


namespace ZigZag
{
    public interface ICollectable : IMonobeh
    {
        void Init( IPooler pooler, Vector3 position );
    }
}