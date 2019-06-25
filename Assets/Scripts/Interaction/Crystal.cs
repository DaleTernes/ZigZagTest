using PubSub;
using UnityEngine;
using ZigZag.Controller;


namespace ZigZag
{
    public class Crystal : PubSubMonobeh, ICollectable, IPoolable
    {
        IPooler _pooler;
        public GameObject GameObject => gameObject;


        public void Init( IPooler pooler, Vector3 position )
        {
            _pooler = pooler;
            gameObject.SetActive( true );
            transform.position = position;
        }


        void OnTriggerEnter( Collider other )
        {
            if ( other.CompareTag( "Player" ) )
            {
                Publish( SimpleEvent.CrystalCollected );
                gameObject.SetActive( false );
                _pooler.AddToPool( this );
            }
        }
    }
}