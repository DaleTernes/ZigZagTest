using UnityEngine;


namespace ZigZag
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;

        Vector3 _offset;


        void Start()
        {
            _offset = transform.position - _target.position;
        }


        void LateUpdate()
        {
            if ( !_target ) return;

            transform.position = _target.position + _offset;
        }
    }
}