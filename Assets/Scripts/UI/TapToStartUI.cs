using PubSub;
using UnityEngine;


namespace ZigZag.UI
{
    public class TapToStartUI : PubSubMonobeh
    {
        [SerializeField] GameObject _gameOver;


        void Awake()
        {
            Subscribe( SimpleEvent.GameOver, OnGameOver );
            _gameOver.SetActive( false );
        }


        void OnGameOver()
        {
            gameObject.SetActive( true );
            _gameOver.SetActive( true );
        }


        public void OnClick()
        {
            Publish( SimpleEvent.StartGame );
            gameObject.SetActive( false );
        }
    }
}