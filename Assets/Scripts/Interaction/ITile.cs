using UnityEngine;
using ZigZag.Controller;


namespace ZigZag
{
    public interface ITile : IMonobeh
    {
        void Init( IPooler pooler, Vector3 position );
        void OnUpdate( Vector3 ballPos );
    }
}