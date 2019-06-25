using UnityEngine;
using ZigZag.Controller;


namespace ZigZag
{
    public class Cube : MonoBehaviour, ITile, IPoolable
    {
        public GameObject GameObject => gameObject;

        const float TIME_TO_WAIT_BEFORE_FALLING = 0.35f;
        const float FREEZE_DEPTH = -10;
        Rigidbody _rigidBody;
        IPooler _pooler;
        bool _fallTimerStarted;
        float _fallTimer;
        bool _hasFallenDeep;


        void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }


        public void Init( IPooler pooler, Vector3 position )
        {
            _pooler = pooler;
            transform.position = position;
            transform.rotation = Quaternion.identity;
            _hasFallenDeep = false;
            _fallTimerStarted = false;
            _fallTimer = 0;
            Freeze();
        }


        void Freeze()
        {
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }


        void Release()
        {
            _rigidBody.constraints = RigidbodyConstraints.None;
        }


        public void OnUpdate( Vector3 ballPos )
        {
            FallCheck( ballPos );
            if ( !_hasFallenDeep && transform.position.y < FREEZE_DEPTH )
            {
                Freeze();
                _pooler.AddToPool( this );
                _hasFallenDeep = true;
            }
        }


        void FallCheck( Vector3 ballPos )
        {
            if ( !_fallTimerStarted )
            {
                var pos = transform.position;
                if ( pos.x < ballPos.x && pos.z < ballPos.z ) _fallTimerStarted = true;
                return;
            }

            _fallTimer += Time.deltaTime;
            if ( _fallTimer < TIME_TO_WAIT_BEFORE_FALLING ) return;

            Release();
            _fallTimerStarted = false;
        }
    }
}