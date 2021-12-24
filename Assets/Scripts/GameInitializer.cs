using System.Collections.Generic;
using Common;
using DefaultNamespace;
using Quiz.Container;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private SymbolContainer _container;
    [SerializeField] private GameView _gameView;
    
    [Header("Circle Parameters")]
    [SerializeField] private Transform _quizPanel;
    [SerializeField] private Circle _circlePrefab;
    [SerializeField] private int _circlesInRows;
    [SerializeField] private float _spacing;
    [SerializeField] private float _duration;

    private int _nowDifficult;
    private GameManager _gameManager;
    private CircleSpawner _circleSpawner;
    private SymbolCollection _symbolCollection;

    private void Start()
    {
        _gameView.StartClick += StartGame;
    }

    private void StartGame()
    {
        InitializeSettings();
        
        _gameManager = new GameManager(_circleSpawner, _symbolCollection);
        _gameView.Init(_gameManager);
    }


    private void InitializeSettings()
    {
        var listCollection = new List<List<SymbolSettings>>
        {
            _container.Letters,
            _container.Numbers
        };

        _circleSpawner = new CircleSpawner(_circlePrefab, _quizPanel, _circlesInRows, _spacing, _duration);
        _symbolCollection = new SymbolCollection(listCollection, _circlesInRows);
    }
}
