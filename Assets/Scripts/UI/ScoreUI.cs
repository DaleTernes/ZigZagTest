using PubSub;
using UnityEngine;
using UnityEngine.UI;


namespace ZigZag.UI
{
    public class ScoreUI : PubSubMonobeh
    {
        [SerializeField] Text _scoreText;
        int _score;


        void Awake()
        {
            Subscribe( SimpleEvent.CrystalCollected, OnCrystalCollected );
            Subscribe( SimpleEvent.StartGame, OnStartGame );
        }


        void OnStartGame()
        {
            _score = 0;
            RefreshScore();
        }


        void OnCrystalCollected()
        {
            _score++;
            RefreshScore();
        }


        void RefreshScore()
        {
            _scoreText.text = _score.ToString();
        }
    }
}