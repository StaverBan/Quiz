using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Text _quizText;
    [SerializeField] private float _bounceScale;
    [SerializeField] private float _duration;

    public event Action StartClick;

    private GameManager _gameManager;
    
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _gameManager.EndGame += OnEndGame;
        _gameManager.ShowQuiz += OnQuizSet;
    }
    
    private void Start()
    {
        _startButton.onClick.AddListener(OnStartClick);
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnStartClick()
    {
        var sequence = DOTween.Sequence();
        
        _startButton.interactable = false;

        sequence.Append(_startButton.transform.DOScale(Vector3.one * _bounceScale, _duration));
        sequence.Append(_startButton.transform.DOScale(Vector3.zero, _duration * 0.2f)).OnKill(OnStartClickLater);
    }

    private void OnStartClickLater()
    {
        StartClick?.Invoke();
        _quizText.DOFade(1, _duration);
    }
    
    private void Restart()
    { 
        var sequence = DOTween.Sequence();
        
        _restartButton.interactable = false;
        
        sequence.Append(_restartButton.transform.DOScale(Vector3.one * _bounceScale, _duration));
        sequence.Append(_restartButton.transform.DOScale(Vector3.zero, _duration * 0.2f)).OnComplete(OnStartClickLater);
    }

    private void OnEndGame()
    {
        _restartButton.interactable = true;
        
        _quizText.DOFade(0, _duration);
        _restartButton.transform.DOScale(Vector3.one, _duration);
        
        _gameManager.EndGame -= OnEndGame;
        _gameManager.ShowQuiz -= OnQuizSet;
    }

    private void OnQuizSet(string text)
    {
        _quizText.text = "Find " + text;
    }
    
}
