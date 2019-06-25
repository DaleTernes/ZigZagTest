using System.Collections.Generic;
using UnityEngine;


namespace ZigZag.Controller
{
    public class PoolController : IPooler
    {
        readonly Queue<IPoolable> _queue = new Queue<IPoolable>();
        readonly List<IPoolable> _items = new List<IPoolable>();
        readonly Object _prefab;
        readonly Transform _holderTransform;


        public PoolController( Object prefab, Transform holderTransform )
        {
            _prefab = prefab;
            _holderTransform = holderTransform;
        }

        public void AddToPool( IPoolable item )
        {
            _queue.Enqueue( item );
        }


        public IPoolable GetItem()
        {
            if ( _queue.Count > 0 )
                return _queue.Dequeue();

            var item = (IPoolable) Object.Instantiate( _prefab, _holderTransform );
            _items.Add( item );
            return item;
        }


        public void Clear()
        {
            foreach ( var item in _items )
                Object.Destroy( item.GameObject );

            _items.Clear();
            _queue.Clear();
        }
    }
}