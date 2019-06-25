using PubSub;
using UnityEngine;


namespace ZigZag
{
    public class Ball : PubSubMonobeh
    {
        const float MIN_PLAYABLE_DEPTH = 0.5f;
        const int GAME_OVER_DEPTH = -4;

        bool _isMovingRight;
        float _speed;


        public void Init( float speed )
        {
            _speed = speed;
            transform.position = Vector3.up;
        }


        public void OnUpdate()
        {
            if ( transform.position.y > MIN_PLAYABLE_DEPTH && Input.anyKeyDown  ) 
                _isMovingRight = !_isMovingRight;

            transform.position += _speed * Time.deltaTime * ( _isMovingRight ? Vector3.right : Vector3.forward );

            if ( transform.position.y < GAME_OVER_DEPTH )
                Publish( SimpleEvent.GameOver );
        }
    }
}