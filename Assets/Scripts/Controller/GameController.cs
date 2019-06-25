using PubSub;
using UnityEngine;


namespace ZigZag.Controller
{
    public class GameController : PubSubMonobeh
    {
        [SerializeField] GameParams _gameParams;
        [SerializeField] Ball _ball;
        [SerializeField] Cube _tilePrefab;
        [SerializeField] Crystal _crystalPrefab;
        
        WorldController _controller;

        bool _gameStarted;


        void Start()
        {
            Subscribe( SimpleEvent.GameOver, OnGameOver );
            Subscribe( SimpleEvent.StartGame, () => _gameStarted = true );
            _controller = new WorldController( _tilePrefab, _crystalPrefab );
            InitWorld();
        }


        void OnGameOver()
        {
            Debug.LogWarning( "The ball has fallen!" );
            _gameStarted = false;
            InitWorld();
        }


        void InitWorld()
        {
            _controller.Init( _gameParams );
            _ball.Init( _gameParams.Speed );
        }


        void Update()
        {
            if ( _gameStarted )
            {
                _ball.OnUpdate();
                _controller.OnUpdate( _ball.transform.position );
            }
        }
    }
}